using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Provas")]
    public class Prova
    {
        [Key]
        public int ProvaId { get; set; }

        public string  TituloProva { get; set; }

        public double ValorProva { get; set; }

        public double NotaProva { get; set; }

        public int StatusProva { get; set; }

        public DateTime DataProva { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }

        public ConfigPln ConfigPln { get; set; }

        public List<RespostasAluno> RespostasAlunos { get; set; }
        public Professor Professor { get; set; }
        public List<Aluno> Alunos  { get; set; }
    }
}