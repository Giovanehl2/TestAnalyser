using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Questoes")]
    public class NotaQuestao
    {
        public NotaQuestao()
        {
            Questao = new Questao();
            Prova = new Prova();
    }
        [Key]
        public int NotaQuestaoId { get; set; }
        public Questao Questao { get; set; }
        public double ValorQuestao { get; set; }
        public Prova Prova { get; set; }
    }
}