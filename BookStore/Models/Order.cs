using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar la fecha.")]
        [Display(Name ="Fecha")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Debe ingresar un cliente.")]
        [Display(Name ="Cliente")]
        public Customer Customer { get; set; }

        [Required(ErrorMessage ="Debe ingresar el total.")]
        public double Total { get; set; }
    }
}
