using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;

namespace Backend.Infrastructure.Repositories
{
    public class ContractViewModelRepository : IReadOnlyRepository<ContractViewModel>, IWriteRepository<ContractViewModel>
    {
        private readonly ConfigurationContext _db;

        public ContractViewModelRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(ContractViewModel viewModel)
        {
            if (viewModel != null)
            {
                // Contract
                var contract = AddContract(viewModel);
                if (contract == null)
                    return false;

                // Signed Contract
                var signedContract = AddSignedContract(viewModel, contract);
                if (signedContract == null)
                    return false;

                // Contract Beneficiaries
                if (AddContractBeneficiaries(viewModel, signedContract))
                {
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private bool AddContractBeneficiaries(ContractViewModel viewModel, SignedContract signedContract)
        {
            var beneficiaries = new List<Guid>();

            var BeneficiaryIndividuals = new List<Individual>();
            var BeneficiaryRealties = new List<Realty>();
            var BeneficiaryMobiles = new List<MobileDevice>();
            var BeneficiaryPets = new List<Pet>();
            var BeneficiaryVehicles = new List<Vehicle>();

            switch (viewModel.Type)
            {
                case Core.Enums.ContractType.DentalPlan:
                case Core.Enums.ContractType.HealthPlan:
                case Core.Enums.ContractType.LifeInsurance:
                    beneficiaries = _db.Individuals
                        .Where(ind => viewModel.Beneficiaries.Contains(ind.BeneficiaryId))
                        .Select(ind => ind.BeneficiaryId)
                        .ToList();

                    foreach (var ind in viewModel.BeneficiaryIndividuals)
                    {
                        if (!IndividualIsValid(ind))
                            return false;
                    }

                    break;
                case Core.Enums.ContractType.AnimalHealthPlan:
                    beneficiaries = _db.Pets
                        .Where(pet => viewModel.Beneficiaries.Contains(pet.BeneficiaryId))
                        .Select(pet => pet.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.MobileDeviceInsurance:
                    beneficiaries = _db.MobileDevices
                        .Where(mob => viewModel.Beneficiaries.Contains(mob.BeneficiaryId))
                        .Select(mob => mob.BeneficiaryId)
                        .ToList();

                    foreach (var mobile in viewModel.BeneficiaryMobiles)
                    {
                        if (!MobileDeviceIsValid(mobile))
                            return false;
                    }

                    break;
                case Core.Enums.ContractType.RealStateInsurance:
                    beneficiaries = _db.Realties
                        .Where(rea => viewModel.Beneficiaries.Contains(rea.BeneficiaryId))
                        .Select(rea => rea.BeneficiaryId)
                        .ToList();

                    foreach (var realty in viewModel.BeneficiaryRealties)
                    {
                        if (!RealtyIsValid(realty))
                            return false;
                    }

                    break;
                case Core.Enums.ContractType.VehicleInsurance:
                    List<Vehicle> vehicles = new List<Vehicle>();
                    beneficiaries = _db.Vehicles
                        .Where(vec => viewModel.Beneficiaries.Contains(vec.BeneficiaryId))
                        .Select(vec => vec.BeneficiaryId)
                        .ToList();

                    foreach (var vehicle in viewModel.BeneficiaryVehicles)
                    {
                        if (!VehicleIsValid(vehicle))
                            return false;
                        vehicles.Add(vehicle);
                    }
                    AddVehicles(vehicles);
                    break;
                default:
                    return false;
            }
            //if (beneficiaries.Count == 0 || beneficiaries.Count != viewModel.Beneficiaries.Count)
            //    return false;

            foreach (var ben in beneficiaries)
            {
                var signedContracts = _db
                                      .SignedContracts
                                      .Where(sc => sc.SignedContractId == _db
                                                                          .Contract_Beneficiary
                                                                          .Where(cb => cb.BeneficiaryId == ben)
                                                                          .Select(cb => cb.SignedContractId).FirstOrDefault()
                     && sc.ContractIndividualIsActive)
                     .ToList();

                foreach(var beneficiarySignedContract in signedContracts)
                {
                    if (_db.Contracts.Where(con => con.ContractId == beneficiarySignedContract.ContractId && con.ContractType == viewModel.Type).Any())
                        return false;
                }

                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContract.SignedContractId,
                    ContractBeneficiaryId = Guid.NewGuid()
                };
                _db.Contract_Beneficiary.Add(contract_beneficiary);
            }
            return true;
        }

        private void AddVehicles(List<Vehicle> listToSave)
        {
            _db.Vehicles.AddRange(listToSave);
        }

        private SignedContract AddSignedContract(ContractViewModel viewModel, Contract contract)
        {
            var contractHolder = _db
                .Individuals
                .Where(ind => ind.BeneficiaryId == viewModel.ContractHolderId)
                .FirstOrDefault();
            if (contractHolder == null)
                return null;

            var signedContract = new SignedContract()
            {
                ContractId = contract.ContractId,
                SignedContractId = Guid.NewGuid(),
                IndividualId = contractHolder.BeneficiaryId,
                ContractIndividualIsActive = viewModel.IsActive
            };
            return _db.SignedContracts.Add(signedContract).Entity;
        }

        private Contract AddContract(ContractViewModel contractViewModel)
        {
            var contract = ViewModelCreator.ContractFactory.Create(contractViewModel);
            if (contract != null)
                return _db.Contracts.Add(contract).Entity;
            return null;
        }

        public ContractViewModel Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContractViewModel> Get()
        {
            List<ContractViewModel> viewModelToReturn = new List<ContractViewModel>();

            var contracts = _db.Contracts
                .Where(con => !con.ContractDeleted)
                .ToList();
            foreach (var contract in contracts)
            {
                var signedContracts = _db
                    .SignedContracts
                    .Where(sc => sc.ContractId == contract.ContractId)
                    .ToList();

                foreach (var signedContract in signedContracts)
                {
                    var beneficiaries = _db
                        .Contract_Beneficiary
                        .Where(cb => cb.SignedContractId == signedContract.SignedContractId)
                        .Select(cb => cb.BeneficiaryId)
                        .ToList();

                    var viewModelToAdd = new ContractViewModel()
                    {
                        Category = contract.ContractCategory,
                        ExpiryDate = contract.ContractExpiryDate,
                        IsActive = signedContract.ContractIndividualIsActive,
                        Type = contract.ContractType,
                        SignedContractId = signedContract.SignedContractId,
                        ContractHolderId = signedContract.IndividualId,
                        Beneficiaries = beneficiaries
                    };
                    viewModelToReturn.Add(viewModelToAdd);
                }
            }
            return viewModelToReturn;
        }

        public bool Remove(Guid id)
        {
            var contractToDelete = _db
                .SignedContracts
                .Where(sc => sc.SignedContractId == id)
                .FirstOrDefault()
                .ContractId;

            if (_db.SignedContracts
                .Where(sc => sc.ContractId == contractToDelete && sc.ContractIndividualIsActive)
                .Count() > 0)
                return false;

            _db.Contracts.Remove(_db.Contracts.Where(c => c.ContractId == contractToDelete).FirstOrDefault());
            _db.SaveChanges();

            return true;
        }

        public ContractViewModel Update(Guid id, ContractViewModel contractViewModel)
        {
            var contractToUpdate = _db.SignedContracts.Where(sc => sc.SignedContractId == id).FirstOrDefault();

            contractToUpdate.ContractIndividualIsActive = contractViewModel.IsActive;


            _db.SaveChanges();
            return contractViewModel;
        }

        private ContractViewModel UpdateContract(Guid id, ContractViewModel contractViewModel)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var contractToUpdate = _db.SignedContracts.Where(sc => sc.SignedContractId == id).FirstOrDefault();

                contractToUpdate.ContractIndividualIsActive = contractViewModel.IsActive;

                var beneficiariesToDelete = _db
                    .Contract_Beneficiary
                    .Where(cb => cb.SignedContractId == contractToUpdate.SignedContractId)
                    .ToList();

                _db.RemoveRange(beneficiariesToDelete);

                if (UpdateBeneficiaries(contractViewModel, contractToUpdate.SignedContractId))
                {
                    _db.SaveChanges();
                    return contractViewModel;
                }

                scope.Complete();
                return null;
            }
        }

        private bool UpdateBeneficiaries(ContractViewModel viewModel, Guid signedContractId)
        {
            var beneficiaries = new List<Guid>();
            switch (viewModel.Type)
            {
                case Core.Enums.ContractType.DentalPlan:
                case Core.Enums.ContractType.HealthPlan:
                case Core.Enums.ContractType.LifeInsurance:
                    beneficiaries = _db.Individuals
                        .Where(ind => viewModel.Beneficiaries.Contains(ind.BeneficiaryId))
                        .Select(ind => ind.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.AnimalHealthPlan:
                    beneficiaries = _db.Pets
                        .Where(pet => viewModel.Beneficiaries.Contains(pet.BeneficiaryId))
                        .Select(pet => pet.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.MobileDeviceInsurance:
                    beneficiaries = _db.MobileDevices
                        .Where(mob => viewModel.Beneficiaries.Contains(mob.BeneficiaryId))
                        .Select(mob => mob.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.RealStateInsurance:
                    beneficiaries = _db.Realties
                        .Where(rea => viewModel.Beneficiaries.Contains(rea.BeneficiaryId))
                        .Select(rea => rea.BeneficiaryId)
                        .ToList();
                    break;
                case Core.Enums.ContractType.VehicleInsurance:
                    beneficiaries = _db.Vehicles
                        .Where(vec => viewModel.Beneficiaries.Contains(vec.BeneficiaryId))
                        .Select(vec => vec.BeneficiaryId)
                        .ToList();
                    break;
                default:
                    return false;
            }
            if (beneficiaries.Count == 0 || beneficiaries.Count != viewModel.Beneficiaries.Count)
                return false;

            foreach (var ben in beneficiaries)
            {
                var signedContracts = _db
                      .SignedContracts
                      .Where(sc => sc.SignedContractId == _db
                                                          .Contract_Beneficiary
                                                          .Where(cb => cb.BeneficiaryId == ben)
                                                          .Select(cb => cb.SignedContractId).FirstOrDefault()
                    && sc.ContractIndividualIsActive)
                    .ToList();

                foreach (var beneficiarySignedContract in signedContracts)
                {
                    if (_db.Contracts.Where(con => con.ContractId == beneficiarySignedContract.ContractId && con.ContractType == viewModel.Type).Any())
                        return false;
                }

                var contract_beneficiary = new ContractBeneficiary()
                {
                    BeneficiaryId = ben,
                    SignedContractId = signedContractId,
                    ContractBeneficiaryId = Guid.NewGuid()
                };
                _db.Contract_Beneficiary.Add(contract_beneficiary);
            }
            return true;
        }

        #region IndividualValidations
        public static bool IndividualIsValid(Individual individual)
        {
            if (!CPFIsValid(individual.IndividualCPF))
                return false;

            if (!EmailIsValid(individual.IndividualEmail))
                return false;

            if (!DateIsValid(individual.IndividualBirthdate))
                return false;
            return true;
        }

        public static bool CPFIsValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool DateIsValid(DateTime date)
        {
            return date != null ? date < DateTime.Today : false;
        }
        #endregion IndividualValidations

        #region RealtyValidations
        public static bool RealtyIsValid(Realty realty)
        {
            if (realty.RealtyMarketValue < 0 && realty.RealtySaleValue < 0)
                return false;

            //if (!CEPIsValid(realty.RealtyAddress.AddressZipCode))
            //    return false;

            if (!DateIsValid(realty.RealtyConstructionDate))
                return false;

            return true;
        }

        public static bool CEPIsValid(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, "[0-9]{5}-[0-9]{3}");
        }
        #endregion RealtyValidations

        #region VehicleValidations
        public static bool VehicleIsValid(Vehicle vehicle)
        {
            if (!DateIsValid(vehicle.VehicleManufactoringYear))
                return false;

            if (!DateIsValid(vehicle.VehicleModelYear))
                return false;

            if (vehicle.VehicleCurrentFipeValue < 0 && vehicle.VehicleCurrentMileage <= 0)
                return false;

            return true;
        }
        #endregion VehicleValidations

        #region MobileDeviceValidations
        public static bool MobileDeviceIsValid(MobileDevice mobileDevice)
        {
            if (!DateIsValid(mobileDevice.MobileDeviceManufactoringYear))
                return false;

            if (mobileDevice.MobileDeviceInvoiceValue <= 0)
                return false;

            return true;
        }
        #endregion MobileDeviceValidations
    }
}
