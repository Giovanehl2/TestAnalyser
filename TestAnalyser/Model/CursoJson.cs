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

        public int CursoId { get; set; }
        public string nomeCurso { get; set; }
        public string descricao { get; set; }

    }

}