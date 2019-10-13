using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel()
        {
            this.Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.GenreId = movie.GenreId;
            this.Name = movie.Name;
            this.NumberInStock = movie.NumberInStock;
            this.ReleaseDate = movie.ReleaseDate;      
        }

        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        [Required]
        public int? NumberInStock { get; set; }

        public byte? GenreId { get; set; }

        public string Title
        {
            get
            {
                return (Id != 0)
                    ? "Edit Movie"
                    : "New Movie";               
            }
        }
    }
}