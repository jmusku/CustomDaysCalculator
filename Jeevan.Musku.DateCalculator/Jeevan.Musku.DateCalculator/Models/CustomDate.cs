using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Jeevan.Musku.DateCalculator.Validators;

namespace Jeevan.Musku.DateCalculator.Models
{
    public class CustomDate
    {
        public CustomDate(string from, string to)
        {
            this.FromDate = from;
            this.ToDate = to;
        }

        [Required]
        [DateValidator]
        public string FromDate { get; set; }
        [Required]
        [DateValidator]
        public string ToDate { get; set; }

    }
}