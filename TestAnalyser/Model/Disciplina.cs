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
        [Key]
        public int DisciplinaId { get; set; }
        public string nome { get; set; }
        public string descrição { get; set; }
        public List<Turma> Turmas { get; set; }
        public List<Professor> Professores { get; set; }

        public List<Curso> Cursos { get; set; }
    }
}