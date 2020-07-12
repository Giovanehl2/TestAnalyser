using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("ConfigsPln")]
    public class ConfigPln
    {
        [Key]
        public int ConfigPlnId { get; set; }
        public int IncorretoInicio { get; set; }
        public int IncorretoFim { get; set; }
        public int RevisarInicio { get; set; }
        public int RevisarFim { get; set; }
        public int CorretoInicio { get; set; }
        public int CorretoFim { get; set; }


    }
}