using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class CursoJson
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string Situacao { get; set; }

    }

}