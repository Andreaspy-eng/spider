using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace spider.Yandex
{
    public class CrUpResultToken
    {
        public DateTime CreationDate { get; set; }
        [Required]
        public string yandex_id { get; set; }
        public string message { get; set; }
    }
}
