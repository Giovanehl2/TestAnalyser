using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Alternativas")]
    public class Alternativas
    {
        public Alternativas()
        {
            Questao =  new Questao();
        }
        [Key]
        public int AlternativasId { get; set; }
        public string DescAlternativa { get; set; }
        public int correto { get; set; }
        public Questao Questao { get; set; }

    }
}