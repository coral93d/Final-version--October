using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetRoleBasedSecurity.Models;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetRoleBasedSecurity.Models
{
    public class PostModel
    {
        public IEnumerable<Tags> Track { get; set; }
        public Tags TrackDetails { get; set; }
        [Display(Name = "Id")]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z,.][a-zA-Z\s,.]+$", ErrorMessage = "This field accepts only alphabetical characters")]
        [Required(ErrorMessage = "Title Is missing.")]
        [MaxLength(50)]
        public string Title{ get; set; }

        [RegularExpression(@"^[a-zA-Z,.][a-zA-Z\s,.]+$", ErrorMessage = "This field accepts only alphabetical characters")]
        [Required(ErrorMessage = "Subject is missing. ")]
        public string Subject { get; set; }

        [Display(Name = "Your Answer")]
        public string ans { get; set; }
        
        [Display(Name = "User_name")]
        public string user { get; set; }

        public string Gender { get; set; }

        [MaxLength(15)]
        public string Samples { get; set; }

        public DateTime DatePosted { get; set; }     

        [Display(Name = "Status")]
        public string Status { get; set; }
        public IEnumerable<SelectListItem> Statuslist { get; set; }

        public string[] Tags { get; set; }
        public string getTags { get; set; }

        public MultiSelectList AllTags { get; set; }

       
        public int PageView { get; set; }
        public string Approver_name { get; set; }



    }
   
    public class ArticlesComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public DateTime? ThisDateTime { get; set; }
        public int ArticleId { get; set; }
        public int? Rating { get; set; }
        public string User_n { get; set; }
        public bool Status_Com { get; set; }
        public bool Com_mark { get; set; }

    }


    public class PostDetails
    {

        public IEnumerable<ArticlesComments> Comments { get; set; }
        public IEnumerable<PostModel> Post { get; set; }
        public IEnumerable<PostModel> popular { get; set; }
        public IEnumerable<Tags> Track { get; set; }
        public IEnumerable<Users> tagfilter { get; set; }
        public IEnumerable<PostModel> Filter { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<Profileee> UserProfilee { get; set; }


    }

    public class Profileee
    {
        public string User_name { get; set; }
        public string user { get; set; }
        [Required(ErrorMessage = "Display Name Is missing.")]
        [Display(Name = "Display Name")]
        public string Dname { get; set; }

        [Required(ErrorMessage = "About Is missing.")]
        [Display(Name = "Little About me")]
        public string About { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LogDate { get; set; }


        [Required(ErrorMessage = "Position Is missing.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Company Name Is missing.")]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Location Is missing.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Email Is missing.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gender Is missing.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Role Is missing.")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Processs Is missing.")]
        public IEnumerable<SelectListItem> Processs { get; set; }

        [Required(ErrorMessage = "Processs Is missing.")]
        public string[] Process { get; set; }
        [Required(ErrorMessage = "Processs Is missing.")]
        public string getProcess { get; set; }
        [Required(ErrorMessage = "Processs Is missing.")]
        public MultiSelectList AllProcess { get; set; }
        [Display(Name = "Profile Picture Theme")]
        [Required(ErrorMessage = "ProfilePictureTheme Is missing.")]
        public string ProfilePictureTheme { get; set; }



    }
    public class Users
    {
        public string userr { get; set; }
        public string User_name { get; set; }
        public string user { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public string Dname { get; set; }

        [Required]
        [Display(Name = "Little About me")]
        public string About { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LogDate { get; set; }


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

        public IEnumerable<SelectListItem> Processs { get; set; }

        [Required]
        public string[] Process { get; set; }
        public string getProcess { get; set; }

        public MultiSelectList AllProcess { get; set; }

        [Display(Name = "Profile Picture Theme")]
        [Required(ErrorMessage = "ProfilePictureTheme Is missing.")]
        public string ProfilePictureTheme { get; set; }


    }
    public class AllUsers
    {
        public IEnumerable<Users> Users { get; set; }
    }

    public class Tags
    {
        public int Id { get; set; }
        public string Track_Name { get; set; }

        public string Descripition { get; set; }
        public string On_time { get; set; }
        public string BC { get; set; }
        public string CC { get; set; }
        public string EU { get; set; }
        public string Updates { get; set; }
    }


    public class Tagg
    {
        public IEnumerable<Tags> Track { get; set; }
    }

    public class search
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }



    }

   

}