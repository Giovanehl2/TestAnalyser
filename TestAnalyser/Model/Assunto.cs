using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Assuntos")]
    public class Assunto
    {
        [Key]
        public int AssuntoId { get; set; }
        public string Descricao { get; set; }
    }
}