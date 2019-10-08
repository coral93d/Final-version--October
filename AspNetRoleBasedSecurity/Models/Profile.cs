using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetRoleBasedSecurity.Models
{
    public class Profile
    {
        public string user { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string Dname { get; set; }

        [Required]
        [Display(Name = "Little About me")]
        public string About { get; set; }

        [Required]
        public string Process { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Role { get; set; }













    }
}