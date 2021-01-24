using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }
    }
}
