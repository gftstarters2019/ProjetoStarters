using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class CompleteContract
    {
        public Contract Contract { get; set; }
        public SignedContract SignedContract { get; set; }
        public List<Individual> Individuals { get; set; }
        public List<Pet> Pets { get; set; }
        public List<MobileDevice> MobileDevices { get; set; }
        public List<Realty> Realties { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
