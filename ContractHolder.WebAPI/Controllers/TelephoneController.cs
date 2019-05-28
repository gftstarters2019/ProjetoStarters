﻿using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContractHolder.WebAPI.Controllers
{
    /// <summary>
    /// Telephone Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneController : Controller
    {
        private readonly IReadOnlyRepository<Telephone> _telephoneReadOnlyRepository;
        private readonly IWriteRepository<Telephone> _telephoneWriteRepository;

        /// <summary>
        /// TelephoneController constructor
        /// </summary>
        /// <param name="telephoneReadoOnlyRepository"></param>
        /// <param name="telephoneWriteRepository"></param>
        public TelephoneController(IReadOnlyRepository<Telephone> telephoneReadoOnlyRepository, IWriteRepository<Telephone> telephoneWriteRepository)
        {
            _telephoneReadOnlyRepository = telephoneReadoOnlyRepository;
            _telephoneWriteRepository = telephoneWriteRepository;
        }
        
        /// <summary>
        /// Gets all telephones registered
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Telephones()
        {
            return Ok(_telephoneReadOnlyRepository.Get());
        }

        /// <summary>
        /// Get a specific telephone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Telephone(Guid id)
        {
            var obj = _telephoneReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Telephone in the database
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        [HttpPost("Telephone")]
        public IActionResult PostTelephone([FromBody] Telephone telephone)
        {
            telephone.TelephoneId = Guid.NewGuid();

            if (Validate(telephone))
            {
                _telephoneWriteRepository.Add(telephone);

                return Ok(telephone);
            }
            else
                throw new InvalidCastException("Numero de telefone inválido!");

        }

        /// <summary>
        /// Updates a Telephone in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="telephone"></param>
        /// <returns></returns>
        [HttpPut("Telephone/{id}")]
        public IActionResult UpdateTelephone(Guid id, [FromBody] Telephone telephone)
        {
            var obj = (Telephone)_telephoneReadOnlyRepository.Find(id);

            obj.TelephoneNumber = telephone.TelephoneNumber;
            obj.TelephoneType = telephone.TelephoneType;

            if (Validate(telephone))
            {
                return Ok(_telephoneWriteRepository.Update(obj));
            }
            else
                throw new InvalidCastException("Numero de telefone inválido!");
        }

        private bool Validate(Telephone telephone)
        {
            foreach (var t in telephone.TelephoneNumber)
            {
                if (t < '0' || t > '9')
                    return false;
            }

            if (telephone.TelephoneNumber.Length < 8 && telephone.TelephoneNumber.Length > 11)
                return false;

            if (!Enum.IsDefined(typeof(TelephoneType), telephone.TelephoneType))
                return false;

            return true;
        }

    }
}