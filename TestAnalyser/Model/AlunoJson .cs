using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{

    public class AlunoJson
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Matricula { get; set; }
        public string Email { get; set; }

    }
}