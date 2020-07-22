using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Professores")]
    public class Professor
    {

        public Professor()
        {
            Disciplinas = new List<Disciplina>();
            Provas = new List<Prova>();
        }
        [Key]
        public int ProfessorId { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public string cpf { get; set; }
        public string Email { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

        public List<Prova> Provas { get; set; }

    }
}