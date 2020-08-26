using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Alunos")]
    public class Aluno
    {
        public Aluno()
        {
            Turmas = new List<Turma>();
            //Provas = new List<Prova>();
        }
        [Key]
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public int Matricula { get; set; }
        public string Email { get; set; }

        public List<Turma> Turmas { get; set; }

        //public List<Prova> Provas { get; set; }

    }
}