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
        [Key]
        public int AlunoId { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public List<Turma> Turmas { get; set; }

        public List<Prova> Provas { get; set; }

    }
}