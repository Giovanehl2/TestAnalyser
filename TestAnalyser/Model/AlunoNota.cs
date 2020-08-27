using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("AlunoNotas")]
    public class AlunoNota
    {
        public AlunoNota()
        {
            Prova = new Prova();
            Aluno = new Aluno();
            //Provas = new List<Prova>();
        }
        [Key]
        public int AlunoNotaId { get; set; }
        public Prova Prova { get; set; }
        public Aluno Aluno { get; set; }
        public double NotaTotal { get; set; }
    }
}