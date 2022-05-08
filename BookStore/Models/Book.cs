using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar el autor.")]
        [Display(Name ="Autor")]
        public Author Author { get; set; }

        [Required(ErrorMessage ="Debe ingresar la editorial.")]
        [Display(Name ="Editorial")]
        public Publisher Publisher { get; set; }

        [Required(ErrorMessage ="Debe ingresar el género.")]
        [Display(Name = "Género")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage ="Debe ingresar un título.")]
        [Display(Name = "Título")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage ="Debe ingresar el ISBN.")]
        [StringLength(100)]
        public string Isbn { get; set; }

        [Required(ErrorMessage ="Debe ingresar stock.")]
        public int Stock { get; set; }

        [Required(ErrorMessage ="Debe ingresar el precio.")]
        [Display(Name ="Precio")]
        public double Price  { get; set; }

       
        [Display(Name = "Fecha de publicación")]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public string Image { get; set; }
    }
}
