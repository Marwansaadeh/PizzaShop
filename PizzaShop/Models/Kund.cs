using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required(ErrorMessage = "Please Write your name")]

        public string Namn { get; set; }
        [Required(ErrorMessage = "Please Write your gatuadress")]

        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Please Write your post number")]

        public string Postnr { get; set; }
        [Required(ErrorMessage = "Please Write your post ort")]

        public string Postort { get; set; }
        [Required(ErrorMessage = "Please Write your email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Write your telephone number")]

        public string Telefon { get; set; }
        [Required(ErrorMessage = "Please Write your user name")]

        public string AnvandarNamn { get; set; }
        [Required(ErrorMessage = "Please Write your password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$", ErrorMessage = "The {0} does not meet requirements.")]
        [DataType(DataType.Password)]
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
