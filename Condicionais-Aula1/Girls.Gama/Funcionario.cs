using System;
using System.Collections.Generic;
using System.Text;

namespace Girls.Gama
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }

        public bool MaiorDeIdade()
        {
            return Idade >= 18;
        }
    }
}
