using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PM.Data.Entity
{
    public class TitleType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "En Fazla 50 Karakter Girilebilir.")]
        public string Title { get; set; }
    }
}
