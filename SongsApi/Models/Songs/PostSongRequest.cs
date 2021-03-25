using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Models.Songs
{
    public class PostSongRequest : IValidatableObject
    {
        [Required][StringLength(100)]
        public string Title { get; set; }
        public string Artist { get; set; }
        [Required]
        public string RecommendedBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(Title.ToLower() == "walk on the ocean" && Artist.ToLower() == "toad the wet sprocket")
            {
                yield return new ValidationResult("I Hate that Song", new string[]
                {
                    nameof(Title), nameof(Artist)
                });
            }
        }
    }

}
