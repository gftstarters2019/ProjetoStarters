﻿using Backend.Application.Singleton;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ContractHolder.WebAPI.Controllers
{
    /// <summary>
    /// Contract Holder API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractHolderController : ControllerBase
    {
        private readonly IReadOnlyRepository<Individual> _contractHolderReadOnlyRepository;
        private readonly IWriteRepository<Individual> _contractHolderWriteRepository;
        private readonly IReadOnlyRepository<ContractHolderViewModel> _contractHolderViewModelReadOnlyRepository;
        private readonly IWriteRepository<ContractHolderViewModel> _contractHolderViewModelWriteRepository;
        private readonly IReadOnlyRepository<SignedContract> _contractsReadOnlyRepository;

        /// <summary>
        /// ContractHolderController constructor
        /// </summary>
        /// <param name="contractHolderReadOnlyRepository"></param>
        /// <param name="contractHolderWriteRepository"></param>
        /// <param name="contractsReadOnlyRepository"></param>
        public ContractHolderController(IReadOnlyRepository<Individual> contractHolderReadOnlyRepository, IWriteRepository<Individual> contractHolderWriteRepository, IReadOnlyRepository<ContractHolderViewModel> contractHolderViewModelReadOnlyRepository, IWriteRepository<ContractHolderViewModel> contractHolderViewModelWriteRepository, IReadOnlyRepository<SignedContract> contractsReadOnlyRepository)
        {
            _contractHolderReadOnlyRepository = contractHolderReadOnlyRepository;
            _contractHolderWriteRepository = contractHolderWriteRepository;
            _contractHolderViewModelReadOnlyRepository = contractHolderViewModelReadOnlyRepository;
            _contractHolderViewModelWriteRepository = contractHolderViewModelWriteRepository;
            _contractsReadOnlyRepository = contractsReadOnlyRepository;
        }

        /// <summary>
        /// Gets all Contract Holders
        /// </summary>
        /// <returns>All Contract Holders</returns>
        [HttpGet]
        public IActionResult ContractHolders()
        {
            return Ok(_contractHolderReadOnlyRepository.Get());
        }

        /// <summary>
        /// Gets a single Contract Holder
        /// </summary>
        /// <param name="id">GUID of the chosen Contract Holder</param>
        /// <returns>Chosen Contract Holder</returns>
        [HttpGet("{id}")]
        public IActionResult ContractHolder(Guid id)
        {
            var obj = _contractHolderReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Contract Holder
        /// </summary>
        /// <param name="vm">Contract Hodler to be created</param>
        /// <returns>Created Contract Holder</returns>
        [HttpPost]
        public IActionResult PostContractHolder([FromBody] ContractHolderViewModel vm)
        {
            //if (!ContractHolderIsValid(individual))
            //    return StatusCode(403);

            if (!_contractHolderViewModelWriteRepository.Add(vm))
                return StatusCode(403);

            //SendWelcomeEmail(vm);
            return Ok(vm);
        }

        /// <summary>
        /// Updates a Contract Holder
        /// </summary>
        /// <param name="id">GUID of the chosen Contract Holder</param>
        /// <param name="individual">Updated Contract Holder object</param>
        /// <returns>Updated Contract Holder</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateContractHolder(Guid id, [FromBody] ContractHolderViewModel newIndividualViewModel)
        {
            //var oldContractHolder = _contractHolderReadOnlyRepository.Find(id);

            //if (oldContractHolder != null)
            //    return StatusCode(403);

            //var newIndividual = ViewModelCreator.IndividualFactory.Create(newIndividualViewModel);
            
            //if(newIndividual != null)
            //{
            //    oldContractHolder.IndividualBirthdate
            //    oldContractHolder.IndividualCPF
            //    oldContractHolder.IndividualEmail
            //    oldContractHolder.IndividualName
            //    oldContractHolder.IndividualRG
            //}


            //if (_contractHolderWriteRepository.Update(oldContractHolder) != null)
            //    return Ok();
            return StatusCode(403);







            //
            //if (!ContractHolderIsValid(individual))
            //    return StatusCode(403);

            //var obj = _contractHolderReadOnlyRepository.Find(id);

            //obj.IndividualBirthdate = individual.IndividualBirthdate;
            //obj.IndividualCPF = individual.IndividualCPF;
            //obj.IndividualEmail = individual.IndividualEmail;
            //obj.IndividualName = individual.IndividualName;
            //obj.IndividualRG = individual.IndividualRG;

            //return Ok(_contractHolderWriteRepository.Update(obj));

        }

        /// <summary>
        /// Soft deletes a Contract Holder from the DB
        /// </summary>
        /// <param name="id">GUID of the Contract Holder</param>
        /// <returns>Deleted Contract Holder</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteContractHolder(Guid id)
        {
            if (_contractsReadOnlyRepository.Get().Where(sc => sc.IndividualId == id).ToList().Count > 0)
                return Forbid();

            var contractHolder = _contractHolderReadOnlyRepository.Find(id);

            if (contractHolder != null)
            {
                contractHolder.IsDeleted = !contractHolder.IsDeleted;
                return Ok(_contractHolderWriteRepository.Update(contractHolder));
            }
            else return NotFound(contractHolder);
        }

        #region Validations
        /// <summary>
        /// Does all validations to see if Contract Holder is valid.
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public static bool ContractHolderIsValid(Individual individual)
        {
            if (!CPFIsValid(individual.IndividualCPF))
                return false;

            if (!EmailIsValid(individual.IndividualEmail))
                return false;

            //if (!RGIsValid(individual.IndividualRG))
            //    return false;

            return true;
        }

        /// <summary>
        /// Algorithm to verify if a string is a CPF
        /// </summary>
        /// <param name="cpf">String to be verified</param>
        /// <returns>If the string is a CPF</returns>
        public static bool CPFIsValid(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Verifies if an email is valid.
        /// </summary>
        /// <param name="emailaddress">Email to be verified</param>
        /// <returns>If email is valid</returns>
        public static bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        //public static bool RGIsValid(string rg)
        //{
        //    if (!new Regex("^\\d{2}.\\d{3}.\\d{3}-\\d$").IsMatch(rg))
        //        return false;

        //    return true;
        //}
        #endregion Validations

        #region SendEmail
        /// <summary>
        /// Sends welcome email to Contract Holder
        /// </summary>
        /// <param name="vm">Individual to send the email</param>
        //public void SendWelcomeEmail(ContractHolderViewModel vm)
        //{
        //    new EmailService().SendEmail("Welcome!",
        //        $"Welcome {vm.IndividualName}!",
        //        vm.IndividualEmail);
        //}
        #endregion SendEmail
    }
}
