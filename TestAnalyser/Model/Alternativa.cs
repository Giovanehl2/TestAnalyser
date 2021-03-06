﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Alternativas")]
    public class Alternativa
    {
        public Alternativa()
        {
            Questao = new Questao();
            RespostaAluno = new List<RespostasAluno>();
        }
        [Key]
        public int AlternativaId { get; set; }
        public string DescAlternativa { get; set; }
        public int correto { get; set; }
        public List<RespostasAluno> RespostaAluno { get; set; }
        public Questao Questao { get; set; }


    }
}