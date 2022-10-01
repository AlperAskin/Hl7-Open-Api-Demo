﻿using Endava.Hl7.Fhir.Common.Core.Errors;
using Hl7.Fhir.Model;
using Endava.Hl7.Fhir.OpenAPI.Controllers.Base;
using Endava.Hl7.Fhir.OpenAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Endava.Hl7.Fhir.OpenAPI.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [EnableCors("EnableCORS")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MedicationController : BaseController<MedicationController>
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(IFhirService fhirService, IMedicationService medicationService)
        {
            _medicationService = medicationService;
            fhirService.Initialize();
        }

        /// <summary>
        /// Get Patient's Medications
        /// </summary>
        /// <param name="patientId">Patient resource Id</param>
        /// <returns>Return list of Medications</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Medication>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{patientId}", Name = "GetPatientMedications")]
        public async Task<IActionResult> GetPatientMedicationsAsync(string patientId)
        {
            Logger.LogDebug(nameof(GetPatientMedicationsAsync));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var medications = await _medicationService.GetMedicationDataForPatientAsync(patientId).ConfigureAwait(false);
            if (medications == null)
            {
                return NotFound(new NotFoundError("The patient medications was not found"));
            }
            return Ok(medications);
        }

        /// <summary>
        /// Add Patient Medication
        /// </summary>
        /// <param name="patientId">Patient resource Id</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [Route("/api/v1/medication/PutPatientMedications/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Medication>))]
        public virtual IActionResult PutPatientMedications([FromRoute][Required] string patientId)
        {
            string exampleJson = null;
            exampleJson = "[ {\n  \"key\" : \"\"\n}, {\n  \"key\" : \"\"\n} ]";

            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Dictionary<string, Object>>>(exampleJson)
            : default(List<Dictionary<string, Object>>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }

    }


