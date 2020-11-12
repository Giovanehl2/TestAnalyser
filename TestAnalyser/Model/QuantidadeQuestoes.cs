using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAnalyser.DAL
{
    public class QuantidadeQuestoes
    {
        //1 = Simples Escolha, 2 = Multipla Escolha, 3 = Verdadeiro ou Falso, 4 = Discursiva.

        public int SimplesEscolha { get; set; }
        public int MultiplaEscolha { get; set; }
        public int VerdadeiroFalso { get; set; }
        public int Discursiva { get; set; }

    }
}