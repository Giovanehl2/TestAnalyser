﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public int Matricula { get; set; }

        public string Senha { get; set; }

        public int TipoUsr { get; set; }

        public Aluno Alunos { get; set; }
        public Professor Professor { get; set; }
        public Configuracao Configuracao { get; set; }


    }
}