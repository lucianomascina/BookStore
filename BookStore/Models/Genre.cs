using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el género.")]
        [Display(Name = "Género")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
