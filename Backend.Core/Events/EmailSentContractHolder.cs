using Backend.Core.Domains;

namespace Backend.Core.Events
{
    public class EmailSentContractHolder
    {
        public EmailSentContractHolder(ContractHolderDomain contractHolderDomain)
        {
            ContractHolderDomain = contractHolderDomain;
        }

        public ContractHolderDomain ContractHolderDomain { get; set; }
    }
}
