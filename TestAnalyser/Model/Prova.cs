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
            NotasQuestoes = new List<NotaQuestao>();
            ConfigPln = new ConfigPln();
            Disciplina = new Disciplina();
        }
        [Key]
        public int ProvaId { get; set; }

        [Required(ErrorMessage = "Campo titulo da prova é obrigatório!")]
        public string TituloProva { get; set; }

        [Required(ErrorMessage = "Campo assuntos é obrigatório!")]
        [NotMapped]
        public List<string> Assuntos { get; set; }


        public int QtSimplesEscolha { get; set; }
        public int QtMultiplaEscolha { get; set; }
        public int QtDissertativa { get; set; }
        public int QtVerdadeirFalso { get; set; }

        [Required(ErrorMessage = "Campo nome da disciplina é obrigatório!")]
        [NotMapped]
        public string NomeDisciplina { get; set; }

        [Required(ErrorMessage = "Campo nome da turma é obrigatório!")]
        [NotMapped]
        public string NomeTurma { get; set; }

        [Required(ErrorMessage = "Campo valor da prova é obrigatório!")]
        public double ValorProva { get; set; }


        public double NotaProva { get; set; }

        public int StatusProva { get; set; }

        [NotMapped]
        public double ConfirmaNota { get; set; }


        [Required(ErrorMessage = "Campo data da prova é obrigatório!")]
        public DateTime DataProva { get; set; }

        [Required(ErrorMessage = "Campo nome horario de inicio da prova é obrigatório!")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "Campo horario termino da prova é obrigatório!")]
        public DateTime HoraFim { get; set; }

        public ConfigPln ConfigPln { get; set; }
        public List<NotaQuestao> NotasQuestoes { get; set; }
        public Disciplina Disciplina { get; set; }
        public List<RespostasAluno> RespostasAlunos { get; set; }
        public Professor Professor { get; set; }
    }
}