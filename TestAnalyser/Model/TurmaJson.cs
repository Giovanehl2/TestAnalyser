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
        public int TurmaId { get; set; }
        public string NomeTurma { get; set; }
        public string Periodo { get; set; }

    }
}