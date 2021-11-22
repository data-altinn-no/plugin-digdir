using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Nadobe;
using Nadobe.Common.Interfaces;
using Nadobe.Common.Models;
using Nadobe.Common.Util;

namespace Altinn.Dan.Plugin.Digdir
{
    public class Main
    {
        private ILogger _logger;
        private readonly EvidenceSourceMetadata _metadata;

        public Main(ILogger logger, IEvidenceSourceMetadata metadata)
        {
            _logger = logger;
            _metadata = (EvidenceSourceMetadata)metadata;
        }

        [Function("TestConsentWithAccessMethod")]
        public async Task<IActionResult> TestConsentWithAccessMethod([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithRequirement")]
        public async Task<IActionResult> TestConsentWithRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithSoftConsentRequirement")]
        public async Task<IActionResult> TestConsentWithSoftConsentRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithConsentAndSoftLegalBasisRequirement")]
        public async Task<IActionResult> TestConsentWithConsentAndSoftLegalBasisRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }

        [Function("TestConsentWithMultipleConsentAndSoftLegalBasisRequirements")]
        public async Task<IActionResult> TestConsentWithMultipleConsentAndSoftLegalBasisRequirements([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(null, GetEvidenceValues);
        }


        [Function(Constants.EvidenceSourceMetadataFunctionName)]
        public async Task<HttpResponseData> Metadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            _logger = context.GetLogger(context.FunctionDefinition.Name);
            _logger.LogInformation($"Running metadata for {Constants.EvidenceSourceMetadataFunctionName}");
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(_metadata.GetEvidenceCodes());
            return response;
        }

        private async Task<List<EvidenceValue>> GetEvidenceValues()
        {
            return await Task.FromResult(new List<EvidenceValue>
            {
                new()
                {
                    EvidenceValueName = "field1",
                    Value = "somevalue"
                }
            });
        }
    }
}
