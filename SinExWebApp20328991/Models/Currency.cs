using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    [Table("Currency")]
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string CurrencyCode { get; set; }
        public virtual double ExchangeRate { get; set; }
        //Navigation property to Desitinations
        public virtual ICollection<Destination> Destination { get; set; }
    }
}