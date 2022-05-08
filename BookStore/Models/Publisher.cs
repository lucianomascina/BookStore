using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre.")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar un país.")]
        [Display(Name = "País")]
        [StringLength(100)]
        public string Country { get; set; }
    }
}
