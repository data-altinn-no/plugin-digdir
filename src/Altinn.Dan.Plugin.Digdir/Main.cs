using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Altinn.Dan.Plugin.Digdir.Models;
using Dan.Common;
using Dan.Common.Interfaces;
using Dan.Common.Models;
using Dan.Common.Util;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Altinn.Dan.Plugin.Digdir
{
    public class Main
    {
        private ILogger _logger;
        private readonly EvidenceSourceMetadata _metadata;
        private const string Source = "Digdir";

        public Main(IEvidenceSourceMetadata metadata)
        {
            _metadata = (EvidenceSourceMetadata)metadata;
        }

        [Function("TestConsentWithAccessMethod")]
        public async Task<HttpResponseData> TestConsentWithAccessMethod([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(req, GetEvidenceValues);
        }

        [Function("TestConsentWithRequirement")]
        public async Task<HttpResponseData> TestConsentWithRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(req, GetEvidenceValues);
        }

        [Function("TestConsentWithSoftConsentRequirement")]
        public async Task<HttpResponseData> TestConsentWithSoftConsentRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(req, GetEvidenceValues);
        }

        [Function("TestConsentWithConsentAndSoftLegalBasisRequirement")]
        public async Task<HttpResponseData> TestConsentWithConsentAndSoftLegalBasisRequirement([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(req, GetEvidenceValues);
        }

        [Function("TestConsentWithMultipleConsentAndSoftLegalBasisRequirements")]
        public async Task<HttpResponseData> TestConsentWithMultipleConsentAndSoftLegalBasisRequirements([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            return await EvidenceSourceResponse.CreateResponse(req, GetEvidenceValues);
        }

        [Function("SimpleEvidence")]
        public async Task<HttpResponseData> SimpleEvidence([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            var evidenceValues = new List<EvidenceValue>
            {
                new()
                {
                    EvidenceValueName = "name",
                    Source = Source,
                    Value = Guid.NewGuid().ToString()
                },
                new()
                {
                    EvidenceValueName = "number",
                    Source = Source,
                    Value = new Random().Next(1, 11)
                },
            };
            return await EvidenceSourceResponse.CreateResponse(req, () => Task.FromResult(evidenceValues));
        }

        [Function("RichEvidence")]
        public async Task<HttpResponseData> RichEvidence([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            // Just some random values for fun
            var mainName = Guid.NewGuid().ToString();
            var randomNumber = new Random().Next(1, 11);
            var richEvidence = new RichEvidence
            {
                Name = Guid.NewGuid().ToString(),
                Number = randomNumber,
                TrueOrFalse = randomNumber % 2 == 0,
                SubEvidence = new()
                {
                    SubName = "Sub-" + mainName,
                    SubNumber = ((double)randomNumber)/2
                }
            };
            var evidenceValues = new List<EvidenceValue>
            {
                new()
                {
                    EvidenceValueName = "default",
                    Source = Source,
                    Value = richEvidence
                }
            };
            return await EvidenceSourceResponse.CreateResponse(req, () => Task.FromResult(evidenceValues));
        }


        [Function(Constants.EvidenceSourceMetadataFunctionName)]
        public HttpResponseData Metadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            _logger = context.GetLogger(context.FunctionDefinition.Name);
            _logger.LogInformation($"Running metadata for {Constants.EvidenceSourceMetadataFunctionName}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(JsonConvert.SerializeObject(_metadata.GetEvidenceCodes(), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore
            }));

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
