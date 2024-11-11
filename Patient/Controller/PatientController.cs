using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Patient.Request_Responce;
using Patient.Service;

namespace Patient.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("Login/{ssn}")]
        public async Task<PatientPayload> Login(string ssn)
        {
            if (String.IsNullOrEmpty(ssn))
            {
                return new PatientPayload(new GeneralResponce(404, "No User Found"));
            }
            try
            { 
                return await _patientService.Login(ssn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PatientPayload(new GeneralResponce(500, "Error in Login"));
            }
        }

        [HttpPost("Register")]
        public async Task<GeneralResponce> Register([FromBody]Patients patients)
        {
            try
            {
                return await _patientService.Register(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in Register");
            }
        }
    }
}