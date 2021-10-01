using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Data.Entity
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
