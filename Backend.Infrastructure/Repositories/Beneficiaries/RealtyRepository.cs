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
    public class RealtyRepository : IRepository<Realty>
    {
        private readonly ConfigurationContext _db;
        private readonly IRepository<Address> _addressRepository;

        public RealtyRepository(ConfigurationContext db,
                                IRepository<Address> addressRepository)
        {
            _db = db;
            _addressRepository = addressRepository;
        }

        public Realty Add(Realty realty)
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
                    realty.Address.AddressId = Guid.NewGuid();
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

        public Realty Find(Guid id)
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
            throw new NotImplementedException();
        }

        public Realty Update(Guid id, Realty t)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Realty> IRepository<Realty>.Get()
        {
            throw new NotImplementedException();
        }
    }
}
