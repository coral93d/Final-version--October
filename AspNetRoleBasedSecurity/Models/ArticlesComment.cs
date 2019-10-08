using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetRoleBasedSecurity.Models
{
    public class ArticlesComment
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

        public string Marked_by { get; set; }

        public DateTime? Marked_on { get; set; }


    }
}