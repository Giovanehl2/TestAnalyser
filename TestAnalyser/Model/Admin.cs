using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public Usuario Usuario { get; set; }
    }
}