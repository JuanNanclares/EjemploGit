using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OralData.Shared.Entities
{
    public class Student
    {


        public int Id { get; set; } //primary key
        [Display(Name = "Tipo de estudiante")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public string Name { get; set; } = null!;
    }
}

