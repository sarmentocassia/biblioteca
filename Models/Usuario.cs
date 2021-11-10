using System;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public String Login { get; set; }

        public String Senha {get; set;}

        public DateTime DataNascimento{get;set;}
    }
}