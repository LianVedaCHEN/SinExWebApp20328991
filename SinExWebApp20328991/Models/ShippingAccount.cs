using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    [Table("ShippingAccount")]
    public class ShippingAccount
    {
        public virtual int ShippingAccountId { get; set; }
        [Required(ErrorMessage ="The Email field is required")]
        [StringLength(30, ErrorMessage ="The length of email address should be less than 30")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public virtual string Email { get; set; }

        [StringLength(10, ErrorMessage = "The length of user name should be less than 10")]
        public virtual string UserName { get; set; }

        [Required(ErrorMessage = "The phone number field is required")]
        [StringLength(14,MinimumLength =8, ErrorMessage = "The number of phone number digits should be between 8 and 14")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Only digits allowed")]
        [Display(Name ="Phone Number")]
        public virtual string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Building field is required")]
        [StringLength(50, ErrorMessage = "The length of building address should be less than 50")]
        public virtual string Building { get; set; }

        [Required(ErrorMessage = "The Street field is required")]
        [StringLength(35, ErrorMessage = "The length of string address should be less than 35")]
        public virtual string Street { get; set; }

        [Required(ErrorMessage = "The City field is required")]
        [StringLength(25, ErrorMessage = "The length of city filed should be less than 25")]
        public virtual string City { get; set; }

        [Required(ErrorMessage = "The Province field is required")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The length of province code should be 2")]
        public virtual string Province { get; set; }

        [Required(ErrorMessage = "The Postal Code field is required")]
        [StringLength(6, MinimumLength = 5, ErrorMessage = "The number of postal code digits should be between 5 and 6")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="postal number can only be digits")]
        [Display(Name = "Postal Number")]
        public virtual string PostalCode { get; set; }

        [Required(ErrorMessage = "The Card type field is required")]
        [RegularExpression(@"^American Express$|^Diners Club$|^Discover$|^MasterCard$|^UnionPay$|^Visa$",ErrorMessage = "The card type can only be Diners Club or Discover or MasterCard or UnionPay or Visa")]
        [Display(Name = "Type")]
        public virtual string CardType { get; set; }

        [Required(ErrorMessage = "The Card Number field is required")]
        [StringLength(19, MinimumLength = 14, ErrorMessage = "The number digits of card number  should be between 14 and 19")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "card number can only be digits")]
        [Display(Name = "Number")]
        public virtual string CardNumber { get; set; }

        [Required(ErrorMessage = "The Security field is required")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "The number digits of security number should be between 3 and 4")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "security number can only be digits")]
        [Display(Name = "Security Number")]
        public virtual string SecurityNumber { get; set; }

        [Required(ErrorMessage = "The Card holder name field is required")]
        [StringLength(70, ErrorMessage = "The length of building address should be less than 70")]
        [Display(Name = "Cardholder Number")]
        public virtual string CardholderName{get;set;}

        [Required(ErrorMessage = "The Expiry Month field is required")]
        [Range(1,12)]
        [Display(Name = "Expiry Month")]
        public virtual string ExpiryMonth { get; set; }
        [Required(ErrorMessage = "The Expiry Year field is required")]
        [Range(1990, 2020)]
        [Display(Name = "Expiry Year")]
        public virtual string ExpiryYear { get; set; }

        //navigation property to Shipments
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}