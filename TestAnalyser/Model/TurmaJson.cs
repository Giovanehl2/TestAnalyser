using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class TurmaJson
    {
        //precisa de um id para diferenciar das instituições que colocam apenas o nome A, B , C

        public int TurmaId { get; set; }
        public string Nome { get; set; }
        public int Periodo { get; set; }
        public string  Situacao { get; set; }

    }
}