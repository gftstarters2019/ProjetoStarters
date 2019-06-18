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
    public class RealtyRepository : IRepository<RealtyViewModel>
    {
        private readonly ConfigurationContext _db;

        public RealtyRepository(ConfigurationContext db)
        {
            _db = db;
        }

        public bool Add(Realty realty)
        {
            if (realty != null)
            {
                _db.Realties.Add(realty);
                if (_db.SaveChanges() == 1)
                    return true;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Realty Update(Guid id, Realty t)
        {
            throw new NotImplementedException();
        }

        public RealtyViewModel Find(Guid id)
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

        public bool Add(RealtyViewModel t)
        {
            throw new NotImplementedException();
        }

        public RealtyViewModel Update(Guid id, RealtyViewModel t)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
