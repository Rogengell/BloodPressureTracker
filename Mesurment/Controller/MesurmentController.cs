using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mesurment.Request_Responce;
using Mesurment.Service;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Mesurment.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MesurmentController : ControllerBase
    {
        private readonly IMesurmentService _mesurmentService;

        public MesurmentController(IMesurmentService mesurmentService)
        {
            _mesurmentService = mesurmentService;
        }

        [HttpGet("GetAllUserMeasurements/{ssn}")]
        public async Task<MeasurmentListPayload> GetAllUserMeasurements(string ssn)
        {
            if (String.IsNullOrEmpty(ssn))
            {
                throw new ArgumentNullException(nameof(ssn));
            }

            try
            {
                var result = await _mesurmentService.GetAllUserMeasurements(ssn);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error in GetAllUserMeasurements");
            }
        }

        [HttpPut("CreateMeasurements")]
        public async Task<GeneralResponce> CreateMeasurements([FromBody]Measurements measurement) 
        {
            try
            {
                var result = await _mesurmentService.CreateMeasurements(measurement);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in CreateMeasurements");
            }
        }

        [HttpDelete("DeleteMeasurements/{id}")]
        public async Task<GeneralResponce> DeleteMeasurements(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            try
            {
                var result = await _mesurmentService.DeleteMeasurements(id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in DeleteMeasurements");
            }
        }

        [HttpPost("UpdateMeasurements/{id}")]
        public async Task<GeneralResponce> updateMeasurements([FromBody]Measurements measurement)
        {
            try
            {
                var result = await _mesurmentService.updateMeasurements(measurement);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in updateMeasurements");
            }
        }

        [HttpPut("CreateRandomMeasurements/{ssn}")]
        public async Task<GeneralResponce> CreateRandomMeasurements(string ssn)
        {
            try
            {
                var Mesurment = new Measurements();
                Mesurment.Datetime = DateTime.Now;
                Mesurment.Systolic = new Random().Next(100,160);
                Mesurment.Diastolic = new Random().Next(60,100);;
                Mesurment.PatientSSN = ssn;
                Mesurment.Seen = false;

                var result = await _mesurmentService.CreateMeasurements(Mesurment);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in CreateRandomMeasurements");
            }  
        }
        
    }
}