using SistemaDePagamento_Aula2_GamaAcademy.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDePagamento_Aula2_GamaAcademy
{
    class Program
    {
        private static List<Boleto> listaBoletos;
        private static List<AVista> listaAVista;
        public bool Autenticacao { get; set; }
        static void Main(string[] args)
        {
            listaBoletos = new List<Boleto>();
            listaAVista = new List<AVista>();

            while (true)
            {
                Console.WriteLine("****************************************");
                Console.WriteLine("****Loja das meninas da Gama Academy****");
                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1-Compra | 2-Pagamento| 3-Relatório");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
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

        public static void Comprar()
        {
            Console.WriteLine("Digite o Valor da compra:");
            var valor = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = Console.ReadLine();

            Console.WriteLine("Preencha a descrição caso necessário:");
            var descricao = Console.ReadLine();

            Console.WriteLine("Escolha a forma de pagamento");
            Console.WriteLine("1-À Vista | 2-Boleto ");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CompraAVista();
                    break;
                case 2:
                    CompraBoleto();
                    break;
                default:
                    break;
            }

            void CompraAVista()
            {
                var avista = new AVista(cpf, valor, descricao);
                avista.GerarAutenticacao();

                Console.WriteLine($"Codigo de autenticação numero {avista.CodigoAutenticacao} gerado com sucesso! Utilize-o para realizar o pagamento!");
                listaAVista.Add(avista);
            }

            void CompraBoleto()
            {
                var boleto = new Boleto(cpf, valor, descricao);
                boleto.GerarBoleto();
            
                Console.WriteLine($"Boleto gerado com sucesso com o numero {boleto.CodigoBarra} com data de vencimento para para o dia {boleto.DataVencimento}");
                listaBoletos.Add(boleto);
            }
        }

        public static void Pagamento()
        {
            Console.WriteLine("Escolha a forma de pagamento");
            Console.WriteLine("1-À Vista | 2-Boleto ");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    PgAVista();
                    break;
                case 2:
                    PgBoleto();
                    break;
                default:
                    break;
            }

            void PgAVista()
            {
                Console.WriteLine("digite o codigo de autenticação:");
                var numeroAv = Guid.Parse(Console.ReadLine());

                var avista = listaAVista
                    .Where(item => item.CodigoAutenticacao == numeroAv)
                    .FirstOrDefault();

                if (avista is null)
                {
                    Console.WriteLine($"Código de autenticãção {numeroAv} não encontrado!");
                    return;
                }

                if (avista.EstaPago())
                {
                    Console.WriteLine("A sua compra já foi paga!");
                    return;
                }
                avista.PagarAVista();
                Console.WriteLine($"O pagamento de numero: {numeroAv} foi realizado com sucesso");
            }

                void PgBoleto()
            { 

                Console.WriteLine("digite o codigo de barras:");
                var numero = Guid.Parse(Console.ReadLine());

                var boleto = listaBoletos
                    .Where(item => item.CodigoBarra == numero)
                    .FirstOrDefault();

                if(boleto is null)
                {
                    Console.WriteLine($"Boleto de codigo {numero} não encontrado!");
                return;
                }

                if(boleto.BoletoEstaPago())
                {
                    Console.WriteLine("O boleto já foi pago!");
                    return;
                }

                if (boleto.DataVencimento < DateTime.Now)
                {
                    boleto.calcularJuros();
                    Console.WriteLine($"O boleto esta vencido, terá acrescimo de 10% *** R$ {boleto.Valor}");
                }

                boleto.Pagar();
                Console.WriteLine($"Boleto de codigo {numero} foi pago com sucesso");
            }
        }
        
        public static void Relatorio()
        {
            Console.WriteLine("Escolha a opção de relatório:");
            Console.WriteLine("1-Pagos | 2-A Pagar | 3-Vencidos");


            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    BoletosPagos();
                    AVistaPagos();
                    break;
                case 2:
                    BoletosAPagar();
                    AVistaAPagar();
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
        public static void AVistaPagos()
        {
            Console.WriteLine("============== A Vista =========== \n");
            var avista = listaAVista
                 .Where(item => item.ConfirmacaoAVista)
                 .ToList();
            foreach (var item in avista)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Barra: {item.CodigoAutenticacao}\nValor:{item.Valor}\n ==");
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

        public static void AVistaAPagar()
        {
            Console.WriteLine("============ A Vista ============");
            var avista = listaAVista
                 .Where(item => item.ConfirmacaoAVista == false)
                 .ToList();

            foreach (var item in avista)
            {
                Console.WriteLine("\n ====");
                Console.WriteLine($"Codigo de Autenticação: {item.CodigoAutenticacao}\nValor:{item.Valor}\n==");
            }
        }
    }
}
