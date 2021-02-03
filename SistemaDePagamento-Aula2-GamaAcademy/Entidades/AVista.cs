using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento_Aula2_GamaAcademy.Entidades
{
    class AVista
    {
        public AVista(string cpf, decimal valor, string descricao)
        {
            Cpf = cpf;
            Valor = valor;
            Descricao = descricao;
        }

        public Guid CodigoAutenticacao { get; set; }
        public decimal Valor { get; set; }
        public bool ConfirmacaoAVista { get; set; }
        public string Cpf { get; set; }
        public string Descricao { get; set; }

        public void GerarAutenticacao()
        {
            CodigoAutenticacao = Guid.NewGuid();
        }

        public bool EstaPago()
        {
            return ConfirmacaoAVista;
        }

        public void PagarAVista()
        {
            ConfirmacaoAVista = true;
        }
    }
}
