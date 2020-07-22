using System;
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
        public Usuario()
        {
            Admin = new Admin();
            Alunos = new Aluno();
            Professor = new Professor();
        }
        [Key]
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        //TipoUsr 1 = Aluno, 2 = Professor, 3 = Admin
        public int TipoUsr { get; set; }

        public Aluno Alunos { get; set; }
        public Professor Professor { get; set; }
        public Admin Admin { get; set; }


    }
}