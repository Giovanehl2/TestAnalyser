﻿using System;
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
        [Key]
        public int ProfessorId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

        public List<Prova> Provas { get; set; }

    }
}