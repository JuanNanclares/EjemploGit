using Microsoft.AspNetCore.Identity;
using OralData.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace OralData.Shared.Entities
{
    public class ClassificationSurvey
    {
        public int Id { get; set; } //primary key
        [Display(Name = "Sintomas")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Synthoms { get; set; } = null!;

        [Display(Name = "Severidad de los sintomas")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string severityOfSymptoms { get; set; } = null!;

        [Display(Name = "Historial medico relevante")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string relevantMedicalHistory { get; set; } = null!;

        [Display(Name = "Otros detalles")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string otherDetails { get; set; } = null!;

    }
}
