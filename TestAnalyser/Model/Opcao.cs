using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Opcoes")]
    public class Opcao
    {
        public Opcao()
        {
            Questao = new Questao();
        }
        [Key]
        public int OpcaoId { get; set; }
        public string descricao { get; set; }
        public Questao Questao { get; set; }
    }
}