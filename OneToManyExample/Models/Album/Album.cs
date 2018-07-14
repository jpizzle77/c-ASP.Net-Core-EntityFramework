using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneToManyExample.Models
{
    public class Album
    {
        [Key]
        public long album_id { get; set; }

        [Display(Name ="Album Title")]
        public string name { get; set; }

        [Display(Name="Release Date")]
        [DataType(DataType.Date)]
        public DateTime release_date { get; set; }

        public long artist_id { get; set; }
        public Artist Artist { get; set; }

    }
}