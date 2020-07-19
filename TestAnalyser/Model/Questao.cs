﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Questoes")]
    public class Questao
    {
        public Questao()
        {
            Disciplina = new Disciplina();
            Opcoes = new List<Opcao>();
            Alternativas = new List<Alternativa>();

        }
        [Key]
        public int QuestaoId { get; set; }
        public string Assunto { get; set; }
        public string Enunciado { get; set; }

        public int TipoQuestao { get; set; }

        /* ativa = 1, inativa = 0*/
        public int situacao { get; set; }
        public string RespostaDiscursiva { get; set; }

        public Disciplina Disciplina { get; set; }

        public List<Opcao> Opcoes { get; set; }

        public List<Alternativa> Alternativas { get; set; }


    }
}