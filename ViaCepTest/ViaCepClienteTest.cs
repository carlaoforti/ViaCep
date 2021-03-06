﻿using System.Net;
using MosaicoSolutions.ViaCep.Net;
using NUnit.Framework;
using Rhino.Mocks;

namespace ViaCepTest
{
    [TestFixture]
    public class ViaCepClienteTest
    {
        private IViaCepCliente _cliente;
        private IViaCepUri _uri;
        private MockRepository _mockRepository;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _cliente = new ViaCepCliente();
            _uri = _mockRepository.Stub<IViaCepUri>();
        }

        [Test]
        public void DeveRetornarUmaRespostaValidaComUmRequisicaoPorCep()
        {
            using (_mockRepository.Record())
            {
                SetupResult.For(_uri.ObterUriComoString()).Return("01001000/json");
            }

            var resposta = _cliente.ObterResposta(_uri);

            Assert.AreEqual(resposta.CodigoDeStatus, HttpStatusCode.OK);
            Assert.True(resposta.EhCodigoDeSucesso);
        }

        [Test]
        public void DeveRetornarUmaRespostaValidaComUmRequisicaoPorEndereco()
        {
            using (_mockRepository.Record())
            {
                SetupResult.For(_uri.ObterUriComoString()).Return("RS/Porto Alegre/Olavo/json");
            }

            var resposta = _cliente.ObterResposta(_uri);

            Assert.AreEqual(resposta.CodigoDeStatus, HttpStatusCode.OK);
            Assert.True(resposta.EhCodigoDeSucesso);
        }

        [Test]
        public void RequisicaoPorCepDeveFalhar()
        {
            using (_mockRepository.Record())
            {
                //Cep não possui um tamanho válido.
                SetupResult.For(_uri.ObterUriComoString()).Return("0100100/json");
            }

            var resposta = _cliente.ObterResposta(_uri);

            Assert.AreEqual(resposta.CodigoDeStatus, HttpStatusCode.BadRequest);
            Assert.False(resposta.EhCodigoDeSucesso);
        }

        [Test]
        public void RequisicaoPorEnderecoDeveFalhar()
        {
            using (_mockRepository.Record())
            {
                //O Nome do logradouro tem menos do que três caracteres.
                SetupResult.For(_uri.ObterUriComoString()).Return("RS/Porto Alegre/Ol/json");
            }

            var resposta = _cliente.ObterResposta(_uri);

            Assert.AreEqual(resposta.CodigoDeStatus, HttpStatusCode.BadRequest);
            Assert.False(resposta.EhCodigoDeSucesso);
        }
    }
}