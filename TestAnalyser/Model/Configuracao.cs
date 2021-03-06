﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;

namespace TestAnalyser.Model
{
    [Table("Configuracoes")]
    public class Configuracao
    {
        [Key]
        public int ConfiguracaoId { get; set; }
        public string UrlApi { get; set; }
        public string sincAuto { get; set; }

        public string HoraCorrecao { get; set; }

        public string Instituicao { get; set; }

        public string Cnpj { get; set; }

        public string ServidorEmail { get; set; }

        public string CaixaSaida { get; set; }

        public string Senha { get; set; }

    }
}