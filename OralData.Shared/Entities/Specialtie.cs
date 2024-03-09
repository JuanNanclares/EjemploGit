using System.ComponentModel.DataAnnotations;

namespace OralData.Shared.Entities
{
    public class Specialtie
    {
        public int Id { get; set; } //primary key

        [Display(Name = "Especialidades odontológicas")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
    }
}