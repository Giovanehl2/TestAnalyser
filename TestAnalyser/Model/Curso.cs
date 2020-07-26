using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Cursos")]
    public class Curso
    {
        public Curso()
        {
            Disciplinas = new List<Disciplina>();
            Turmas = new List<Turma>();
        }


        [Key]
        public int CursoId { get; set; }
        public string NomeCurso { get; set; }
        public string Descricao { get; set; }

        public List<Disciplina> Disciplinas { get; set; }
        public List<Turma> Turmas { get; set; }

    }

}