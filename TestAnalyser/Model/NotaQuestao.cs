using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("NotasQuestoes")]
    public class NotaQuestao
    {
        public NotaQuestao()
        {
            Questao = new Questao();
    }
        [Key]
        public int NotaQuestaoId { get; set; }
        public Questao Questao { get; set; }
        public double ValorQuestao { get; set; }

    }
}