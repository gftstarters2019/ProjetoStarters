using Backend.Core.Enums;
using Backend.Core.Models;
using Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;

namespace ContractHolder.WebAPI.Controllers
{
    /// <summary>
    /// Telephone Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneController : Controller
    {
        private readonly IRepository<TelephoneEntity> _telephoneRepository;

        /// <summary>
        /// TelephoneController constructor
        /// </summary>
        /// <param name="telephoneRepository"></param>
        public TelephoneController(IRepository<TelephoneEntity> telephoneRepository)
        {
            _telephoneRepository = telephoneRepository;
        }
        
        /// <summary>
        /// Gets all telephones registered
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Telephones()
        {
            return Ok(_telephoneRepository.Get());
        }

        /// <summary>
        /// Get a specific telephone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Telephone(Guid id)
        {
            var obj = _telephoneRepository.Find(id);
            return Ok(obj);
        }

        /// <summary>
        /// Creates a new Telephone in the database
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostTelephone([FromBody] TelephoneEntity telephone)
        {
            telephone.TelephoneId = Guid.NewGuid();

            if (Validate(telephone))
            {
                _telephoneRepository.Add(telephone);

                return Ok(telephone);
            }
            else
                return Conflict();

        }

        // <summary>
        // 
        // </summary>
        // <param name="telephone"></param>
        // <returns></returns>
        //[HttpPost("Link")]
        //public IActionResult LinkTelephone([FromBody] TelephoneIndividualViewModel telephoneIndividualViewModel)
        //{
        //    //telephone.TelephoneId = Guid.NewGuid();

        //    //if (Validate(telephone))
        //    //{
        //    //    _telephoneWriteRepository.Add(telephone);

        //    //    return Ok(telephone);
        //    //}
        //    //else
        //        return Conflict();

        //}

        /// <summary>
        /// Updates a Telephone in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="telephone"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateTelephone(Guid id, [FromBody] TelephoneEntity telephone)
        {
            var obj = _telephoneRepository.Find(id);

            obj.TelephoneNumber = telephone.TelephoneNumber;
            obj.TelephoneType = telephone.TelephoneType;

            if (Validate(telephone))
            {
                return Ok(_telephoneRepository.Update(id, obj));
            }
            else
                return Conflict();
        }

        private bool Validate(TelephoneEntity telephone)
        {
            Regex regex = new Regex("^[0-9]+$");

            if (!regex.IsMatch(telephone.TelephoneNumber))
                return false;

            if (telephone.TelephoneNumber.Length < 8 || telephone.TelephoneNumber.Length > 11)
                return false;

            if (!Enum.IsDefined(typeof(TelephoneType), telephone.TelephoneType))
                return false;

            return true;
        }

    }
}