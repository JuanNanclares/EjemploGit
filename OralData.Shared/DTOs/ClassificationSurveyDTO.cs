using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OralData.Shared.DTOs
{
    public class ClassificationSurveyDTO
    {
        public int Id { get; set; }

        public string Synthoms { get; set; } = string.Empty;

        public string SeverityOfSymptoms { get; set; } = string.Empty;

        public string RelevantMedicalHistory { get; set; } = string.Empty;

        public string OtherDetails { get; set; } = string.Empty;
    }
}
