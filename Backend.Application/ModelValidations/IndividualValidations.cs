using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Application.ModelValidations
{
    public static class IndividualValidations
    {
        public static bool IndividualIsValid(IndividualEntity individual)
        {
            if (!NameIsValid(individual.IndividualName))
                return false;

            if (!CPFIsValid(individual.IndividualCPF))
                return false;

            if (!EmailIsValid(individual.IndividualEmail))
                return false;

            if (!RGIsValid(individual.IndividualRG))
                return false;

            if (!ValidationsHelper.DateIsValid(individual.IndividualBirthdate))
                return false;

            return true;
        }
        /// <summary>
        /// Validação de Nome
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool NameIsValid(string name)
        {
            // Validando se só tem letras no Nome
            if (!new Regex("^[A-ZÀ-Ÿ][A-zÀ-ÿ']+\\s([A-zÀ-ÿ']\\s?)*[A-ZÀ-Ÿ][A-zÀ-ÿ']+$").IsMatch(name))
                return false;

            return true;
        }

        /// <summary>
        /// Validação de CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private static bool CPFIsValid(string cpf)
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
        /// Validação de Email
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        private static bool EmailIsValid(string emailaddress)
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

        /// <summary>
        /// Validação de RG
        /// </summary>
        /// <param name="rg"></param>
        /// <returns></returns>
        private static bool RGIsValid(string rg)
        {
            //Validando se só tem numeros no RG
            if (!new Regex("^[0-9]+$").IsMatch(rg))
                return false;

            return true;
        }
    }
}
