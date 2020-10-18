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
            Alternativas = new List<Alternativa>();
            //Prova = new Prova();
            Aluno = new Aluno();
        }
        [Key]
        public int RespostasAlunoId { get; set; }
        public string RespostaDiscursiva { get; set; }
        public Questao Questao { get; set; }
        public List<Alternativa> Alternativas  { get; set; }
        public double NotaAluno { get; set; }
        //public Prova Prova { get; set; }
        public Aluno Aluno { get; set; }
        //0 = não corrigido, 1 = correto, 2= parcial ,3 = incorreto , 4 correção manual, 5 silicitado Revisao, 6 ERRO.
        public int SituacaoCorrecao { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }

    }
}