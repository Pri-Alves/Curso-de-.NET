﻿using SistemaDePagamento_Aula2_GamaAcademy.Entidades.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento_Aula2_GamaAcademy.Entidades
{
    class Geladeira : Produto, IComprarProduto
    {
        private const double oferta = 0.15;
        public Geladeira(string nomeProduto, double valorProduto, string cor, double voltagem)
        {
            NomeProduto = nomeProduto;
            ValorProduto = valorProduto;
            Cor = cor;
            Voltagem = voltagem;

        }
        public void Oferta()
        {
            var descontoProduto = ValorProduto * oferta;
            ValorProduto = ValorProduto - descontoProduto - 100;
        }
    }
}
