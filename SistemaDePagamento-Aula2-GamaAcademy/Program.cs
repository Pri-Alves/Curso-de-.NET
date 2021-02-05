using SistemaDePagamento_Aula2_GamaAcademy.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDePagamento_Aula2_GamaAcademy
{
    class Program
    {
        private static List<Boleto> listaBoletos;
        private static List<Dinheiro> listaAVista;
        private static List<Televisor> listaTelevisor;
        private static List<Geladeira> listaGeladeira;
        static void Main(string[] args)
        {
            listaBoletos = new List<Boleto>();
            listaAVista = new List<Dinheiro>();
            listaTelevisor = new List<Televisor>();
            listaGeladeira = new List<Geladeira>();


            while (true)
            {
                Console.WriteLine("****************************************");
                Console.WriteLine("*********** Lojinha Moderna ************");
                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1-Compra | 2-Pagamento de Boleto| 3-Relatório");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        ComprarProduto();
                        break;
                    case 2:
                        Pagamento();
                        break;
                    case 3:
                        Relatorio();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ComprarProduto()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("Escolha o Produto");
            Console.WriteLine("1-Televisor | 2-Geladeira ");

            var nomeProduto = "";
            var valorProduto = 0;

            var opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                nomeProduto = "Televisor";
                valorProduto = 6000;
            }
            else if (opcao == 2)
            {
                 nomeProduto = "Geladeira";
                 valorProduto = 3500;
            }

            Console.WriteLine("\nCor:");
            var cor = Console.ReadLine();

            Console.WriteLine("\nVoltagem (110 / 220) :");
            var voltagem = double.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    EscolhaTelevisor(nomeProduto, valorProduto, cor, voltagem);
                    break;
                case 2:
                    EscolhaGeladeira(nomeProduto, valorProduto, cor, voltagem);
                    break;
                default:
                    break;
            }
        }

        public static void EscolhaTelevisor(string nomeProduto, double valorProduto, string cor, double voltagem)
        {
            var televisor = new Televisor(nomeProduto, valorProduto, cor, voltagem);

            televisor.Oferta();

            listaTelevisor.Add(televisor);

            foreach (var item in listaTelevisor)
            {
                Console.WriteLine($"Parabens! Graças a nossa oferta você esta aquirindo um {item.NomeProduto} de R$6000 por R${item.ValorProduto} de cor {item.Cor} e voltagem {item.Voltagem}v ");
            }
            Comprar();
        }

        public static void EscolhaGeladeira(string nomeProduto, double valorProduto, string cor, double voltagem)
        {
            var geladeira = new Geladeira(nomeProduto, valorProduto, cor, voltagem);
            geladeira.Oferta();
            listaGeladeira.Add(geladeira);
            foreach (var item in listaGeladeira)
            {
                Console.WriteLine($"Parabens! Graças a nossa oferta você esta aquirindo um {item.NomeProduto} de R$3500 por R${item.ValorProduto} de cor {item.Cor} e voltagem {item.Voltagem}v ");
            }
            Comprar();
        }

        public static void Comprar()
        {
            var geladeira = listaGeladeira
                          .FirstOrDefault();

            var televisor = listaTelevisor
                          .FirstOrDefault();

            var valor = televisor != null ? televisor.ValorProduto : geladeira.ValorProduto;

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("Preencha a descrição caso necessário:");
            var descricao = Console.ReadLine();

            Console.WriteLine("****************************");
            Console.WriteLine("Escolha a forma de pagamento");
            Console.WriteLine("1-Dinheiro | 2-Boleto ");

            var opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                var dinheiro = new Dinheiro(valor);
                dinheiro.Pagar();

                Console.WriteLine($"Numero do pagamento {dinheiro.Id} pago no valor: {dinheiro.Valor} ");
                listaAVista.Add(dinheiro);
            }
            else
            {
                var boleto = new Boleto(cpf, valor, descricao);
                boleto.GerarBoleto();

                Console.WriteLine($"Boleto gerado com sucesso com o numero {boleto.CodigoBarra} com data de vencimento para para o dia {boleto.DataVencimento}");
                listaBoletos.Add(boleto);
            }
        }
          
        public static void Pagamento()
        {
       
            Console.WriteLine("digite o codigo de barras:");
            var numero = Guid.Parse(Console.ReadLine());

            var boleto = listaBoletos
                .Where(item => item.CodigoBarra == numero)
                .FirstOrDefault();

            if (boleto is null)
            {
                Console.WriteLine($"Boleto de codigo {numero} não encontrado!");
                return;
            }

            if (boleto.EstaPago())
            {
                Console.WriteLine($"O boleto já foi pago no dia {boleto.DataPagamento}");
                return;
            }

            if (boleto.EstaVencido())
            {
                boleto.CalcularJuros();
                Console.WriteLine($"O boleto esta vencido, terá acrescimo de 10% *** R$ {boleto.Valor}");
            }

            boleto.Pagar();
            Console.WriteLine($"Boleto de codigo {numero} foi pago com sucesso");
        }

        public static void Relatorio()
        {
            Console.WriteLine("Qual opção de relatório:");
            Console.WriteLine("1-Dinheiro | 2-Boleto");


            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    RelatorioAVista();
                    break;
                case 2:
                    RelatorioBoleto();
                    break;
                default:
                    break;
            }

        }

        public static void RelatorioAVista()
        {
            Console.WriteLine("======== Pagamentos à Vista ======== \n");
            var avista = listaAVista
                 .ToList();

            foreach (var item in avista)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Pagamento: {item.Id}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }
        }
        public static void RelatorioBoleto()
        {
            Console.WriteLine("Escolha a opção de relatório:");
            Console.WriteLine("1-Pagos | 2-A Pagar | 3-Vencidos");


            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    BoletosPagos();
                    break;
                case 2:
                    BoletosAPagar();
                    break;
                case 3:
                    BoletosVencidos();
                    break;
                default:
                    break;
            }
        }

        public static void BoletosPagos()
        {
            Console.WriteLine("====== Pagametos Recebidos ======== \n");
            Console.WriteLine("============= Boletos ============= \n");
            var boletos = listaBoletos
                 .Where(item => item.Confirmacao)
                 .ToList();
            foreach (var item in boletos)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }
        }

        public static void BoletosVencidos()
        {
            Console.WriteLine("===== Pagamentos vencidos ======");
            var boletos = listaBoletos
                 .Where(item => item.Confirmacao == false
                 && item.DataVencimento < DateTime.Now)
                 .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }
        }

        public static void BoletosAPagar()
        {
            Console.WriteLine("===== Pagamentos à Receber =======");
            Console.WriteLine("============= Boletos ============");
            var boletos = listaBoletos
                 .Where(item => item.Confirmacao == false
                    && item.DataVencimento > DateTime.Now)
                 .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento: {item.DataPagamento} ==");
            }
        }
    }
}
