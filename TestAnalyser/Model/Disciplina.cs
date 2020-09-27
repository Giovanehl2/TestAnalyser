using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Disciplinas")]
    public class Disciplina
    {
        public Disciplina()
        {
              Turmas = new List<Turma>();
              Professores = new List<Professor>();
              Cursos = new List<Curso>();
              Provas = new List<Prova>();
        }
        
        [Key]
        public int DisciplinaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<Turma> Turmas { get; set; }
        public List<Professor> Professores { get; set; }
        public List<Prova> Provas { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}