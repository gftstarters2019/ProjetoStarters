using Backend.Infrastructure.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Singleton
{
    public sealed class Converters
    {
        private static ConverterContractHolderToModels converterContractHolderToModels = null;
        private static ConverterContractToModels converterContractToModels = null;
        private static ConverterModelsToContractHolder converterModelsToContractHolder = null;
        private static ConverterModelsToContract converterModelsToContract = null;
        private static readonly object padlock = new object();

        public Converters()
        {
        }

        public static ConverterContractHolderToModels ConverterContractHolderToModels
        {
            get
            {
                if (converterContractHolderToModels == null)
                {
                    lock (padlock)
                    {
                        if (converterContractHolderToModels == null)
                        {
                            converterContractHolderToModels = new ConverterContractHolderToModels();
                        }
                    }
                }
                return converterContractHolderToModels;
            }
        }

        public static ConverterContractToModels ConverterContractToModels
        {
            get
            {
                if (converterContractToModels == null)
                {
                    lock (padlock)
                    {
                        if (converterContractToModels == null)
                        {
                            converterContractToModels = new ConverterContractToModels();
                        }
                    }
                }
                return converterContractToModels;
            }
        }

        public static ConverterModelsToContractHolder ConverterModelsToContractHolder
        {
            get
            {
                if (converterModelsToContractHolder == null)
                {
                    lock (padlock)
                    {
                        if (converterModelsToContractHolder == null)
                        {
                            converterModelsToContractHolder = new ConverterModelsToContractHolder();
                        }
                    }
                }
                return converterModelsToContractHolder;
            }
        }

        public static ConverterModelsToContract ConverterModelsToContract
        {
            get
            {
                if (converterModelsToContract == null)
                {
                    lock (padlock)
                    {
                        if (converterModelsToContract == null)
                        {
                            converterModelsToContract = new ConverterModelsToContract();
                        }
                    }
                }
                return converterModelsToContract;
            }
        }
    }
}
