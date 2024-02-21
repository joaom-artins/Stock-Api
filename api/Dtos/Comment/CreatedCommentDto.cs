using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreatedCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Tittle must be 5 characters")]
        [MaxLength(280,ErrorMessage ="Tittle cannot be over 280 characters")]
        public string Title { get; set; }=string.Empty;
        [Required]
        [MinLength(5,ErrorMessage ="Content must be 5 characters")]
        [MaxLength(1024,ErrorMessage ="Content cannot be over 1024 characters")]
        public string Content { get; set; }=string.Empty;
    }
}