using Fernando;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Girls.Gama
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Bem-vindos meninas!");

            Console.WriteLine("============================================");
            Console.WriteLine("Digite o nome:");
            string Nome = Console.ReadLine();

            Console.WriteLine("Digite o CPF:");
            string cpf = Console.ReadLine();

            Console.WriteLine("Digite o telefone:");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite a idade:");
            int idade = int.Parse(Console.ReadLine());

            Estoque estoque = new Estoque();

            Funcionario funcionario = new Funcionario();
            funcionario.Nome = Nome;
            funcionario.CPF = cpf;
            funcionario.Telefone = telefone;
            funcionario.Idade = idade;

            if (funcionario.MaiorDeIdade())
            {
                Console.WriteLine($"Funcionario {funcionario.Nome} é maior de idade");
            }
            else
            {
                Console.WriteLine($"{funcionario.Nome} é menor de idade");
            }



            bool resultado = idade < 18 ? false : true;

            //if (idade <= 18)
            //{
            //    Console.WriteLine("A pessoa é menor de idade");
            //}
            //else if (idade <= 10)
            //{
            //    Console.WriteLine("====");
            //}
            //else
            //{
            //    Console.WriteLine("Pessoa maior de idade");
            //}

            //switch (idade)
            //{
            //    case 10:
            //        Console.WriteLine("====");
            //        break;
            //    case 18:
            //        Console.WriteLine("PEssoa é maior de idade");
            //        break;
            //    default:
            //        break;
            //}

            //while (idade < 18)
            //{
            //    idade++;
            //    Console.WriteLine($"idade agora: {idade}");
            //}

            //do
            //{
            //    idade++;
            //    Console.WriteLine($"idade agora: {idade}");
            //}
            //while (idade == 18);

            //for (int i = idade; i <= 18; i++)
            //{
            //    Console.WriteLine($"idade agora: {i}");
            //}

            //int[] numeros = { 5, 4, 3, 2, 1, 10, 20, 30, 40 };
            //var numerosQuantidade = new int[5];
            //string[] paises = { "Brasil", "Italia", "Alemanha", "Espanha" };
            //List<string> paisesEuropeus = new List<string>();
            //paisesEuropeus.Add("Brasil");
            //paisesEuropeus.Add("Italia");
            //paisesEuropeus.Add("Italia");
            //paisesEuropeus.Add("Alemanha");
            //paisesEuropeus.Add("Espanha");

            //List<string> paisesSulAmericanos = new List<string>();
            //paisesSulAmericanos.Add("Argentina");
            //paisesSulAmericanos.Add("Chile");
            //paisesSulAmericanos.Add("Uruguai");

            ////foreach (var item in paisesSulAmericanos)
            ////{
            ////    paisesEuropeus.Add(item);
            ////}

            //paisesEuropeus.AddRange(paisesSulAmericanos);

            //paisesEuropeus
            //    .Distinct()
            //    .ToList()
            //    .ForEach(item => Console.WriteLine($"Todos os Países => {item}"));

            //int soma = numeros.Sum();
            //int somaFor = 0;

            //for (int i = 0; i < 9; i++)
            //{
            //    somaFor = somaFor + numeros[i];
            //}

            //foreach (var item in paises)
            //{
            //    if (item.Contains("It"))
            //    {
            //        Console.WriteLine($"Nome: {item}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Nome parecido => {item}");
            //    }
            //}

            //var nomePais = from pais in paises
            //               where pais.Contains("It")
            //               select pais;

            //nomePais
            //    .Distinct()
            //    .ToList()
            //    .ForEach(item => Console.WriteLine($"Achou país => {item}"));

            //foreach (var item in nomePais.Distinct())
            //{
            //    Console.WriteLine($"Achou país => {item}");
            //}


            //Console.WriteLine($"Soma: {soma} => For: {somaFor}");


            Console.ReadLine();
        }


    }
}
