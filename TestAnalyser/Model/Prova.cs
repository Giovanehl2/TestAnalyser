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
            NotaQuestoes = new List<NotaQuestao>();
            ConfigPln = new ConfigPln();
            Assuntos = new List<string>();
        }
        [Key]
        public int ProvaId { get; set; }

        public string TituloProva { get; set; }

        public List<string> Assuntos { get; set; }

        public int QtSimplesEscolha { get; set; }
        public int QtMultiplaEscolha { get; set; }
        public int QtDissertativa { get; set; }
        public int QtVerdadeirFalso { get; set; }

        [NotMapped]
        public string NomeDisciplina { get; set; }
        [NotMapped]
        public string NomeTurma { get; set; }
        
        public double ValorProva { get; set; }

        public double NotaProva { get; set; }

        public int StatusProva { get; set; }

        public DateTime DataProva { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }

        public ConfigPln ConfigPln { get; set; }
        public List<NotaQuestao> NotaQuestoes { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<RespostasAluno> RespostasAlunos { get; set; }
        public Professor Professor { get; set; }
    }
}