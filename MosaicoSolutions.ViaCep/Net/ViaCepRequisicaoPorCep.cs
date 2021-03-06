﻿using System;
using MosaicoSolutions.ViaCep.Modelos;

namespace MosaicoSolutions.ViaCep.Net
{
    /// <summary>
    /// Representa uma requisição por Cep.
    /// </summary>
    public sealed class ViaCepRequisicaoPorPorCep : ViaCepRequisicaoPor<Cep>
    {
        internal ViaCepRequisicaoPorPorCep(Cep cep, ViaCepFormatoRequisicao formatoRequisicao) :base(cep, formatoRequisicao)
        {
            if (cep.IsEmpty)
                throw new ArgumentException("O cep não pode estar vazio.");
        }

        public override string ObterUriComoString()
            => $"{ObjetoDaRequisicao}/{FormatoRequisicao.Valor}";
    }
}
