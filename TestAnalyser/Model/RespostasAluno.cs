using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("RespostasAlunos")]
    public class RespostasAluno
    {
        public RespostasAluno()
        {
            Questao = new Questao();
        }
        [Key]
        public int RespostasAlunoId { get; set; }
        public string RespostaDiscursiva { get; set; }

        public Questao Questao { get; set; }
        
    }
}