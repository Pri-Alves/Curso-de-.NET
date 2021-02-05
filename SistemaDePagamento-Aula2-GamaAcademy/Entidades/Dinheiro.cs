using SistemaDePagamento_Aula2_GamaAcademy.Entidades.Interface;
using System;


namespace SistemaDePagamento_Aula2_GamaAcademy.Entidades
{
    public class Dinheiro : Pagamento, IPagar

    {
        private const double Desconto = 0.05;
        public  Dinheiro( double valor)
        {
            Valor = valor; 
        }

        public void Pagar()
        {
            var valorDesconto = Valor * Desconto;
            Valor = Valor - valorDesconto;

            DataPagamento = DateTime.Now;
            Confirmacao = true;
        }
    }
}
