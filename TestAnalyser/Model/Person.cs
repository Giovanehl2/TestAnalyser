using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAnalyser.Model
{
    public class Person
    {
        [Display(Name = "Personal Details:")]
        [Required(ErrorMessage = "Personal Details is required.")]
        [AllowHtml]
        public string PersonalDetails { get; set; }
    }
}