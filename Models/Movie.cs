using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Mike09.Models
{
    //create a class that will hold information about movies
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Rating { get; set; }
        public bool? Edited { get; set; }
        public string LentTo { get; set; }
        [MaxLength(25, ErrorMessage = "Please limit notes to 25 characters or less")]
        public string Notes { get; set; }

    }
}
