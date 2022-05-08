using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar la venta.")]
        [Display(Name = "Venta")]
        public Order Order { get; set; }

        [Required(ErrorMessage = "Debe ingresar el precio.")]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad.")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Debe ingresar un libro.")]
        [Display(Name = "Libro")]
        public Book Book { get; set; }
    }
}
