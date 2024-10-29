using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyECommerece.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Adı & Soyadı")]
        public string FullName { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        //seller icin

    }
}
