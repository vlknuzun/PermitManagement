using PM.Core;
using PM.Core.Repository.ConreateRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PM.Data.Entity
{
    public class Member : MemberRepository
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "İsim Alanı Boş Bırakılamaz.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = true, ErrorMessage = "Soyisim Alanı Boş Bırakılamaz.")]
        public string LastName { get; set; }
        [ForeignKey("Id")]
        public int TitleTypeId { get; set; }
        public int LeavingRight { get; set; }

        public TitleType TitleType { get; set; }
    }
}
