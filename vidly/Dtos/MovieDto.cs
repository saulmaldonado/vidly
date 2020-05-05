using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using vidly.Models;

namespace vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please the movie name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; }
        [Range(1, 20)]
        public int Stock { get; set; }

        public int NumberAvailable { get; set; }
    }
}