using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Configuration;
using Backend.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Infrastructure.Repositories
{
    public class RealtyRepository : IRepository<RealtyEntity>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<AddressEntity> _addressRepository;

        public RealtyRepository(ConfigurationContext db,
                                IRepository<AddressEntity> addressRepository)
        {
            _db = db;
            _addressRepository = addressRepository;
        }

        public RealtyEntity Add(RealtyEntity realty)
        {
            if (realty != null)
            {
                // Verifies if Municipal Registration is already in DB
                if (_db.Realties
                        .Where(real => real.RealtyMunicipalRegistration == realty.RealtyMunicipalRegistration
                                       && !real.IsDeleted)
                        .Any())
                    return null;

                if (realty.Address.AddressId == Guid.Empty)
                {
                    realty.Address.AddressId = _addressRepository.Add(realty.Address).AddressId;
                }

                realty.BeneficiaryId = Guid.NewGuid();
                realty.IsDeleted = false;
                var addedRealty = _db.Realties.Add(realty).Entity;

                if (_db.Beneficiary_Address.Add(new BeneficiaryAddress()
                                                {
                                                    AddressId = realty.Address.AddressId,
                                                    BeneficiaryId = addedRealty.BeneficiaryId,
                                                    BeneficiaryAddressId = Guid.NewGuid()
                                                }).Entity != null)
                    return addedRealty;
            }
            return null;
        }

        public RealtyEntity Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealtyViewModel> Get()
        {
            List<RealtyViewModel> realtiesToReturn = new List<RealtyViewModel>();
            var realties = _db.Realties.ToList();
            foreach (var realty in realties)
            {
                realtiesToReturn.Add(new RealtyViewModel()
                {
                    Id = realty.BeneficiaryId,
                    ConstructionDate = realty.RealtyConstructionDate,
                    MarketValue = realty.RealtyMarketValue,
                    MunicipalRegistration = realty.RealtyMunicipalRegistration,
                    SaleValue = realty.RealtySaleValue,
                    Address = _db
                                    .Addresses
                                    .Where(a => a.AddressId == _db.Beneficiary_Address
                                                                .Where(ba => ba.BeneficiaryId == realty.BeneficiaryId)
                                                                .Select(ba => ba.AddressId)
                                                                .FirstOrDefault())
                                    .FirstOrDefault()
                });
            }
            return realtiesToReturn;
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public RealtyEntity Update(Guid id, RealtyEntity realty)
        {
            if (realty != null)
            {
                // Verifies if Municipal Registration is already in DB
                if (_db.Realties
                        .Where(real => real.RealtyMunicipalRegistration == realty.RealtyMunicipalRegistration
                                       && !real.IsDeleted)
                        .Any())
                    return null;

                var realtyToUpdate = Find(id);
                if (realtyToUpdate != null)
                {
                    // If Address has no ID, add it
                    if (realty.Address.AddressId == Guid.Empty)
                        realty.Address = _addressRepository.Add(realty.Address);
                    // If Address has ID, update it
                    else
                        realty.Address = _addressRepository.Update(realty.Address.AddressId, realty.Address);
                    if (!_addressRepository.Save())
                        return null;

                    realtyToUpdate.IsDeleted = realty.IsDeleted;
                    realtyToUpdate.RealtyConstructionDate = realty.RealtyConstructionDate;
                    realtyToUpdate.RealtyMarketValue = realty.RealtyMarketValue;
                    realtyToUpdate.RealtyMunicipalRegistration = realty.RealtyMunicipalRegistration;
                    realtyToUpdate.RealtySaleValue = realty.RealtySaleValue;

                    var updatedRealty = _db.Realties.Update(realtyToUpdate).Entity;
                    if(updatedRealty != null)
                    {
                        // Delete old relationships
                        _db.Beneficiary_Address.RemoveRange(_db.Beneficiary_Address.Where(ba => ba.BeneficiaryId == id));

                        if (_db.Beneficiary_Address.Add(new BeneficiaryAddress()
                        {
                            AddressId = realtyToUpdate.Address.AddressId,
                            BeneficiaryId = realty.BeneficiaryId,
                            BeneficiaryAddressId = Guid.NewGuid()
                        }) != null)
                        {
                            return updatedRealty;
                        }
                    }
                }
            }
            return null;
        }

        IEnumerable<RealtyEntity> IRepository<RealtyEntity>.Get()
        {
            throw new NotImplementedException();
        }

        public RealtyViewModel FindCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
