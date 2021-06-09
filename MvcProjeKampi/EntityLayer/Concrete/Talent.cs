using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class Talent
    {
        [Key]
        public int TalentID { get; set; }
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Photo { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(15)]
        public string Talent1 { get; set; }

        [StringLength(15)]
        public string Talent2 { get; set; }

        [StringLength(15)]
        public string Talent3 { get; set; }

        [StringLength(15)]
        public string Talent4 { get; set; }

        [StringLength(15)]
        public string Talent5 { get; set; }

        [StringLength(15)]
        public string Talent6 { get; set; }

        [StringLength(15)]
        public string Talent7 { get; set; }

        [StringLength(15)]
        public string Talent8 { get; set; }

        [StringLength(15)]
        public string Talent9 { get; set; }

        [StringLength(15)]
        public string Talent10 { get; set; }

        public int Talent1Val { get; set; }
        public int Talent2Val { get; set; }
        public int Talent3Val { get; set; }
        public int Talent4Val { get; set; }
        public int Talent5Val { get; set; }
        public int Talent6Val { get; set; }
        public int Talent7Val { get; set; }
        public int Talent8Val { get; set; }
        public int Talent9Val { get; set; }
        public int Talent10Val { get; set; }
    }
}
