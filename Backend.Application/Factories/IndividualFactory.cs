using Backend.Application.Interfaces;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class IndividualFactory: IFactory<Individual>
    {
        
        public Individual Create(Guid individualId, string individualName, string individualCpf, string individualRg, string individualEmail, DateTime individualbirthdate, bool individualDeleted)
        {
            var individual = new Individual();
            individual.IndividualId = Guid.NewGuid();
            individual.IndividualName = individualName;
            individual.IndividualCPF = individualCpf;
            individual.IndividualRG = individualRg;
            individual.IndividualEmail = individualEmail;
            individual.IndividualBirthdate = individualbirthdate;
            individual.IndividualDeleted = individualDeleted;

            return individual;
        }

        public Individual Create()
        {
           return new Individual();
        }
    }
}