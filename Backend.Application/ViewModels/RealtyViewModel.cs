using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ViewModels
{
    public class RealtyViewModel
    {
        public Guid Id { get; set; }
        public string MunicipalRegistration { get; set; }
        public DateTime ConstructionDate { get; set; }
        public double SaleValue { get; set; }
        public double MarketValue { get; set; }
        public Address Address { get; set; }
    }
}
