using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class AlunoNota
    {
        public Prova Prova { get; set; }
        public Aluno Aluno { get; set; }
        public double NotaTotal { get; set; }
    }
}