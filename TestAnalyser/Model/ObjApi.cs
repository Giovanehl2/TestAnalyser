using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class ObjApi
    {
        public List<AlunoJson> AlunoJson { get; set; }
        public ProfessorJson ProfessorJson { get; set; }
        public DisciplinaJson DisciplinaJson { get; set; }
        public CursoJson CursoJson { get; set; }
        public TurmaJson TurmaJson { get; set; }
    }
}