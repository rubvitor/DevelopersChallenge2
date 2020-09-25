using System.Collections.Generic;
using System.Threading.Tasks;
using FinantialManager.Application.EventSourcedNormalizers;
using FinantialManager.Application.Interfaces;
using FinantialManager.Application.ViewModels;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.CrossCutting.Util;
using FinantialManager.Infra.CrossCutting.Util.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinantialManager.Services.Api.Controllers
{
    [Authorize]
    public class OFXController : ApiController
    {
        private readonly IOFXAppService _OFXAppService;

        public OFXController(IOFXAppService OFXAppService)
        {
            _OFXAppService = OFXAppService;
        }

        [HttpGet("ofx-management")]
        public async Task<IActionResult> Get()
        {
            return CustomResponse(await _OFXAppService.GetAll());
        }

        [HttpGet("ofx-management/{id:guid}")]
        public async Task<IActionResult> Get(string id)
        {
            return CustomResponse(await _OFXAppService.GetById(id));
        }

        [HttpGet("ofx-management/transactions/{bankid:int}/{accountid}")]
        public async Task<IActionResult> Get(ushort bankid, string accountid)
        {
            string inputAcc = $"{bankid}" +
                              $"{accountid.ToUpper().Replace(" ", string.Empty)}";

            return CustomResponse(await _OFXAppService.GetSTMTTRNByAccountId(inputAcc.GetHash()));
        }

        [HttpGet("ofx-management/transactions/{bankid:int}/{accountid}/total")]
        public async Task<IActionResult> TotalTransactions(ushort bankid, string accountid)
        {
            string inputAcc = $"{bankid}" +
                              $"{accountid.ToUpper().Replace(" ", string.Empty)}";

            var tran = await _OFXAppService.GetSTMTTRNByAccountId(inputAcc.GetHash());

            return CustomResponse(tran.Count);
        }

        [HttpGet("ofx-management/transactions")]
        public async Task<IActionResult> GetSTMTTRN()
        {
            return CustomResponse(await _OFXAppService.GetAllSTMTTRN());
        }

        [HttpPost("ofx-management")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            List<ValidationResult> validationsResults = new List<ValidationResult>();
            foreach (var file in files)
            {
                var arrayFile = OFXUtils.ReadAsArray(file);
                var result = OFXUtils.ReadOFXtoXMLFromArray(arrayFile);

                var ofx = OFX.GetOFXFromString(result);
                ofx.FileOFX = string.Join('\n', result);

                var ofxViewModel = new OFXViewModel
                {
                    OFX = ofx
                };

                validationsResults.Add(await _OFXAppService.Register(ofxViewModel));
            }

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(validationsResults);
        }

        [HttpPut("ofx-management")]
        public async Task<IActionResult> Put(List<IFormFile> files)
        {
            List<ValidationResult> validationsResults = new List<ValidationResult>();
            foreach (var file in files)
            {
                var arrayFile = OFXUtils.ReadAsArray(file);
                var result = OFXUtils.ReadOFXtoXMLFromArray(arrayFile);

                var ofx = OFX.GetOFXFromString(result);
                ofx.FileOFX = string.Join('\n', result);

                var ofxViewModel = new OFXViewModel
                {
                    OFX = ofx
                };

                validationsResults.Add(await _OFXAppService.Update(ofxViewModel));
            }

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(validationsResults);
        }

        [HttpDelete("ofx-management")]
        public async Task<IActionResult> Delete(List<IFormFile> files)
        {
            List<ValidationResult> validationsResults = new List<ValidationResult>();
            foreach (var file in files)
            {
                var arrayFile = OFXUtils.ReadAsArray(file);
                var result = OFXUtils.ReadOFXtoXMLFromArray(arrayFile);

                var ofx = OFX.GetOFXFromString(result);
                ofx.Id = ofx.GenerateOFXId();

                validationsResults.Add(await _OFXAppService.Remove(ofx.Id));
            }

            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(validationsResults);
        }

        [HttpDelete("ofx-management/guid")]
        public async Task<IActionResult> Delete(string id)
        {
            return CustomResponse(await _OFXAppService.Remove(id));
        }

        [HttpGet("ofx-management/history/{id:guid}")]
        public async Task<IList<OFXHistoryData>> History(string id)
        {
            return await _OFXAppService.GetAllHistory(id);
        }
    }
}
