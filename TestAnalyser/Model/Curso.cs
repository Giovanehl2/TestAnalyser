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
        [Key]
        public int CursoId { get; set; }
        public string nomeCurso { get; set; }
        public string descricao { get; set; }

        public List<Disciplina> Disciplinas { get; set; }
    }

}