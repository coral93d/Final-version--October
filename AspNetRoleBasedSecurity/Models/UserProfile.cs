using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetRoleBasedSecurity.Models
{

    public class PostModell
    {

        [Display(Name = "Id")]

        public int Id { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Subject { get; set; }
        [Display(Name = "Your Answer")]
        public string ans { get; set; }
        //[Required]
        // public string Tags { get; set; }
        [Display(Name = "User_name")]
        public string user { get; set; }

        public DateTime DatePosted { get; set; }

        [Display(Name = "Status")]

        public string Status { get; set; }

       
        public string LogDate { get; set; }


        public IEnumerable<SelectListItem> Statuslist { get; set; }
        [Required]
        public string[] Tags { get; set; }
        public string getTags { get; set; }

        public MultiSelectList AllTags { get; set; }
        public int PageView { get; set; }

    }

    public class Profilee
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

    public class ArticlesCommentss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public string Comments { get; set; }

        public DateTime? ThisDateTime { get; set; }

        public int ArticleId { get; set; }

        public int? Rating { get; set; }
        public string User_n { get; set; }
        public DateTime? LogDate { get; set; }
        public bool Status_Com { get; set; }
        public bool Com_mark { get; set; }

    }

    public class MasterDetails
    {

        public IEnumerable<Profilee> UserProfile { get; set; }
        public IEnumerable<PostModell> Post { get; set; }
        public IEnumerable<ArticlesCommentss> Commentg { get; set; }
       


    }

    public class profileDetail
    {
        public IEnumerable<Profilee> UserProfile { get; set; }
     
        public IEnumerable<PostModell> Post { get; set; }

        public IEnumerable<ArticlesCommentss> Comments { get; set; }
      

    }






}