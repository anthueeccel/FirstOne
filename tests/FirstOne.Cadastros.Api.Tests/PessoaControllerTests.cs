using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;

namespace FirstOne.Cadastros.Api.Tests
{
    public class PessoaControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PessoaController _controller;

        public PessoaControllerTests()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<PessoaController>();
        }

        [Fact(DisplayName = "Verifica entidade PessoaController ")]
        [Trait("Cadastro", "PessoaController")]
        public void GetAll_deve_retornar_todos()
        {
            var pessoas = new List<PessoaViewModel>
            {
                new PessoaViewModel() { Id = Guid.NewGuid(),  Nome = "Tester1"}
            };
            _mocker.GetMock<IPessoaAppService>().Setup(e => e.GetAll()).Returns(pessoas);

            var result = _controller.GetAll();

            Assert.Single(result);
        }

        [Fact(DisplayName = "Deve falhar adicionar pessoa")]
        [Trait("Cadastro", "PessoaController")]
        public void Add_deve_falhar_adicionar()
        {
            var pessoa = new PessoaViewModel()
            {
                Nome = ""
            };

            var command = new AddPessoaCommand(pessoa.Nome);
            command.IsValid();
            _mocker.GetMock<IPessoaAppService>().Setup(e => e.Add(It.IsAny<PessoaViewModel>())).Returns(command.ValidationResult);

            var result = _controller.Add(pessoa);

            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);

        }
    }
}
