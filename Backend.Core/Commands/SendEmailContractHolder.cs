using Backend.Core.Domains;

namespace Backend.Core.Commands
{
    public class SendEmailContractHolder
    {
        public SendEmailContractHolder (ContractHolderDomain contractHolderDomain)
        {
            ContractHolderDomain = contractHolderDomain;
        }

        public ContractHolderDomain ContractHolderDomain { get; set; }
    }
}
