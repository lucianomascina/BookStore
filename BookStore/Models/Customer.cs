using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre.")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido.")]
        [Display(Name = "Apellido")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Debe ingresar un e-mail.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Debe ingresar un teléfono.")]
        [Display(Name ="Teléfono")]
        [Phone]
        public string Phone { get; set; }
    }
}
