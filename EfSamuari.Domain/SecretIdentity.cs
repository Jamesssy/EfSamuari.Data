using System;
using System.Collections.Generic;
using System.Text;

namespace EfSamurai.Domain
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public virtual Samurai Samurai { get; set; }
        public string RealName { get; set; }
        public int SamuraiId { get; set; }
    }
   
}
