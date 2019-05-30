﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContractHolder.WebAPI.Controllers
{
    /// <summary>
    /// Address Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IReadOnlyRepository<Address> _addressReadOnlyRepository;
        private readonly IWriteRepository<Address> _addressWriteRepository;

        /// <summary>
        /// AddressController constructor
        /// </summary>
        /// <param name="addressReadoOnlyRepository"></param>
        /// <param name="addressWriteRepository"></param>
        public AddressController(IReadOnlyRepository<Address> addressReadoOnlyRepository, IWriteRepository<Address> addressWriteRepository)
        {
            _addressReadOnlyRepository = addressReadoOnlyRepository;
            _addressWriteRepository = addressWriteRepository;
        }

        /// <summary>
        /// Gets all addresses registered
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Addresses()
        {
            return Ok(_addressReadOnlyRepository.Get());
        }

        /// <summary>
        /// Get a specific address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Address(Guid id)
        {
            var obj = _addressReadOnlyRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Address in the database
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostAddress([FromBody] Address address)
        {
            address.AddressId = Guid.NewGuid();

            if (Validate(address))
            {
                _addressWriteRepository.Add(address);

                return Ok(address);
            }
            else
                return Conflict();

        }

        /// <summary>
        /// Updates a Address in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAddress(Guid id, [FromBody] Address address)
        {
            var obj = (Address)_addressReadOnlyRepository.Find(id);

            obj.AddressStreet = address.AddressStreet;
            obj.AddressNumber = address.AddressNumber;
            obj.AddressComplement = address.AddressComplement;
            obj.AddressNeighborhood = address.AddressNeighborhood;
            obj.AddressCity = address.AddressCity;
            obj.AddressState = address.AddressState;
            obj.AddressCountry = address.AddressCountry;
            obj.AddressZipCode = address.AddressZipCode;

            if (Validate(address))
            {
                return Ok(_addressWriteRepository.Update(obj));
            }
            else
                return Conflict();
        }

        private bool Validate(Address address)
        {
            Regex regexLetters = new Regex("^[a-zA-Z]+$");

            if (!new Regex("^[0-9]+$").IsMatch(address.AddressNumber))
                return false;

            if (!new Regex("^\\d{5}(?:[-\\s]\\d{4})?$").IsMatch(address.AddressZipCode))
                return false;

            if (!regexLetters.IsMatch(address.AddressStreet) || !regexLetters.IsMatch(address.AddressComplement) || !regexLetters.IsMatch(address.AddressNeighborhood) || 
                    !regexLetters.IsMatch(address.AddressCity) || !regexLetters.IsMatch(address.AddressState) || !regexLetters.IsMatch(address.AddressCountry))
                return false;

            return true;
        }
    }
}