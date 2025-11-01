using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mixposAPI.Models.CustomClass
{
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Compania { get; set; }
    }
}