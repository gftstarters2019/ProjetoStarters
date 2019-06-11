using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ViewModels
{
    public class TelephoneIndividualViewModel
    {
        public Guid ContractHolderID { get; set; }
        public List<Guid> TelephonesIDs { get; set; }
    }
}
