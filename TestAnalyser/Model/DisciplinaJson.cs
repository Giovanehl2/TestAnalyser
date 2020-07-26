using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class DisciplinaJson
    {

        public int DisciplinaId { get; set; }
        public string nome { get; set; }
        public string descrição { get; set; }
    }
}