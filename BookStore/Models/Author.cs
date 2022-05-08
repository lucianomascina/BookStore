using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar un nombre.")]
        [Display(Name ="Nombre")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido.")]
        [Display(Name = "Apellido")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name ="Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

    }
}
