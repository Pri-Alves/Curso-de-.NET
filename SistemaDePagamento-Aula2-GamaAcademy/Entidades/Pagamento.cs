﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento_Aula2_GamaAcademy.Entidades
{
    public class Pagamento
    { 

        public Pagamento()
        {
        Id = Guid.NewGuid();       
        }

        public Guid Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Confirmacao { get; set; }
        public double Valor { get; set; }
        public string Cpf { get; set; }
    
    }
}
