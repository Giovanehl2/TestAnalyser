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
        public Prova()
        {
            RespostasAlunos = new List<RespostasAluno>();
            Professor = new Professor();
            Alunos = new List<Aluno>();
            Questoes = new List<Questao>();
            ConfigPln = new ConfigPln();
            Assuntos = new List<string>();
        }
        [Key]
        public int ProvaId { get; set; }

        public string TituloProva { get; set; }

        [NotMapped]
        public List<string> Assuntos { get; set; }

        [NotMapped]
        public int QtSimplesEscolha { get; set; }
        [NotMapped]
        public int QtMultiplaEscolha { get; set; }
        [NotMapped]
        public int QtDissertativa { get; set; }
        [NotMapped]
        public int QtVerdadeirFalso { get; set; }

        public double ValorProva { get; set; }

        public double NotaProva { get; set; }

        public int StatusProva { get; set; }

        public DateTime DataProva { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }

        public ConfigPln ConfigPln { get; set; }

        public List<Questao> Questoes { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<RespostasAluno> RespostasAlunos { get; set; }
        public Professor Professor { get; set; }
        public List<Aluno> Alunos { get; set; }
    }
}