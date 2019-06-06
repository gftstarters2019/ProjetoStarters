using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ViewModels
{
    public class RealtyViewModel
    {
        public string RealtyMunicipalRegistration { get; set; }
        public DateTime RealtyConstructionDate { get; set; }
        public double RealtySaleValue { get; set; }
        public double RealtyMarketValue { get; set; }
        public Address RealtyAddress { get; set; }
    }
}
