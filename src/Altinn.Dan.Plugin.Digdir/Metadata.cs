using System.Collections.Generic;
using Altinn.Dan.Plugin.Digdir.Models;
using Dan.Common.Enums;
using Dan.Common.Interfaces;
using Dan.Common.Models;
using Newtonsoft.Json;
using NJsonSchema;

namespace Altinn.Dan.Plugin.Digdir
{
    public class EvidenceSourceMetadata : IEvidenceSourceMetadata
    {
        public const string Source = "Digdir";
        public const int OrganizationNotFound = 1;
        public const int CcrUpstreamError = 2;

        public List<EvidenceCode> GetEvidenceCodes()
        {
            return new List<EvidenceCode>()
            {                
                new()
                {
                    EvidenceCodeName = "TestConsentWithRequirement",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "test", "ebevis-product" },
                    AuthorizationRequirements = new List<Requirement>
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 2,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        }
                    },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "field1",
                            ValueType = EvidenceValueType.String
                        }
                    }
                },
                new()
                {
                    EvidenceCodeName = "TestConsentWithSoftConsentRequirement",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "test", "ebevis-product" },
                    AuthorizationRequirements = new List<Requirement>
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 2,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true,
                            FailureAction = FailureAction.Skip
                        }
                    },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "field1",
                            ValueType = EvidenceValueType.String
                        }
                    }
                },
                new()
                {
                    EvidenceCodeName = "TestConsentWithConsentAndSoftLegalBasisRequirement",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "test", "ebevis-product" },
                    AuthorizationRequirements = new List<Requirement>
                    {
                        new ConsentRequirement()
                        {
                            ServiceCode = "5616",
                            ServiceEdition = 2,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true
                        },
                        new LegalBasisRequirement()
                        {
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            AppliesToServiceContext = new List<string> { "test" },
                            FailureAction = FailureAction.Skip
                        }
                    },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "field1",
                            ValueType = EvidenceValueType.String
                        }
                    }
                },
                new()
                {
                    EvidenceCodeName = "TestConsentWithMultipleConsentAndSoftLegalBasisRequirements",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "test", "ebevis-product" },
                    AuthorizationRequirements = new List<Requirement>
                    {
                        new ConsentRequirement()
                        {
                            AppliesToServiceContext = new List<string> { "test" },
                            ServiceCode = "5616",
                            ServiceEdition = 2,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true,
                        },
                        new ConsentRequirement()
                        {
                            AppliesToServiceContext = new List<string> { "ebevis-product" },
                            ServiceCode = "5616",
                            ServiceEdition = 1,
                            ConsentPeriodInDays = 90,
                            RequiresSrr = true,
                        },
                        new LegalBasisRequirement()
                        {
                            AppliesToServiceContext = new List<string> { "test"},
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            FailureAction = FailureAction.Skip
                        },
                        new LegalBasisRequirement()
                        {
                            AppliesToServiceContext = new List<string> { "ebevis-product"},
                            ValidLegalBasisTypes = LegalBasisType.Cpv,
                            FailureAction = FailureAction.Deny
                        }
                    },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "field1",
                            ValueType = EvidenceValueType.String
                        }
                    }
                },
                new()
                {
                    EvidenceCodeName = "SimpleEvidence",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "dantest-product" },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "name",
                            Source = Source,
                            ValueType = EvidenceValueType.String
                        },
                        new()
                        {
                            EvidenceValueName = "number",
                            ValueType = EvidenceValueType.Number
                        }
                    }
                },
                new()
                {
                    EvidenceCodeName = "RichEvidence",
                    EvidenceSource = Source,
                    BelongsToServiceContexts = new List<string> { "dantest-product" },
                    Values = new List<EvidenceValue>()
                    {
                        new()
                        {
                            EvidenceValueName = "default",
                            Source = Source,
                            ValueType = EvidenceValueType.JsonSchema,
                            JsonSchemaDefintion = JsonSchema.FromType<RichEvidence>().ToJson(Formatting.None)
                        }
                    }
                }
            };
        }
    }
}
