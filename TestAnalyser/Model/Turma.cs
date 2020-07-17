using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Turmas")]
    public class Turma
    {
        public Turma()
        {
            Disciplinas = new List<Disciplina>();
            Alunos = new List<Aluno>();
            Curso = new Curso();
        }
        [Key]
        public int TurmaId { get; set; }
        public string NomeTurma { get; set; }
        public string Periodo { get; set; }
        public List<Disciplina> Disciplinas { get; set; }

        public List<Aluno> Alunos { get; set; }

        public Curso Curso { get; set; }

    }
}