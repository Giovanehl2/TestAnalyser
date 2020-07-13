using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Questoes")]
    public class Questao
    {
        public Questao()
        {
            Disciplina = new Disciplina();
            Opcoes = new List<Opcao>();
        }
        [Key]
        public int QuestaoId { get; set; }
        public string assunto { get; set; }
        public string Enunciado { get; set; }

        public int TipoQuestao { get; set; }

        public String RespostaDiscursiva { get; set; }

        public Disciplina Disciplina { get; set; }

        public List<Opcao> Opcoes { get; set; }


    }
}