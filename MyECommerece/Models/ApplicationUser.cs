using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyECommerece.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Name & Surname")]
        public string FullName { get; set; }

        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "BirthDate")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        public ICollection<Order> Orders { get; set; }

        //seller icin

    }
}
