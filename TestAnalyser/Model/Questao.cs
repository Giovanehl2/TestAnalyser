﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAnalyser.Model
{
    [Table("Questoes")]
    public class Questao
    {
        public Questao()
        {
            AssuntoQuestao = new Assunto();
            Disciplina = new Disciplina();
            Opcoes = new List<Opcao>();
            Alternativas = new List<Alternativa>();
        }
        [Key]
        public int QuestaoId { get; set; }

        [AllowHtml]
        [Column(TypeName = "Text")]
        public string Enunciado { get; set; }

        //1 = Simples Escolha, 2 = Multipla Escolha, 3 = Verdadeiro ou Falso, 4 = Discursiva.
        public int TipoQuestao { get; set; }

        /* ativa = 1, inativa = 0*/
        public int situacao { get; set; }

        [AllowHtml]
        [Column(TypeName = "Text")]
        public string RespostaDiscursiva { get; set; }

        public Disciplina Disciplina { get; set; }

        public Assunto AssuntoQuestao { get; set; }

        public List<Opcao> Opcoes { get; set; }

        public List<Alternativa> Alternativas { get; set; }


    }
}