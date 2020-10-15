using FirstOne.Cadastros.Api.Controllers;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Messaging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            _controller = new PessoaController(_mocker.GetMock<IPessoaAppService>().Object,
                                               _mocker.GetMock<DomainNotificationHandler>().Object);
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
        public async Task Add_deve_falhar_adicionar()
        {
            var pessoa = new PessoaViewModel()
            {
                Nome = ""
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("Favor informar o Nnome.")
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.HasNotification()).Returns(true);
            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.GetNotifications()).Returns(notifications);

            var result = await _controller.Add(pessoa);

            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }

        [Fact(DisplayName = "Deve adicionar pessoa")]
        [Trait("Cadastro", "PessoaController")]
        public async Task Add_deve_adicionar()
        {
            //Arrange
            var pessoa = new PessoaViewModel()
            {
                Nome = "Nome Teste"
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.HasNotification()).Returns(false);

            //Act
            var result = await _controller.Add(pessoa);

            //Assert
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact(DisplayName = "Deve atualizar pessoa")]
        [Trait("Cadastro", "PessoaController")]
        public async Task Add_deve_atualizar()
        {
            //Arrange
            var pessoa = new PessoaViewModel()
            {
                Id = Guid.NewGuid(),
                Nome = "Nome Teste"
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.HasNotification()).Returns(false);

            //Act
            var result = await _controller.Update(pessoa);

            //Assert
            _mocker.GetMock<DomainNotificationHandler>().Verify(dn => dn.HasNotification(), Times.Once);
            _mocker.GetMock<IPessoaAppService>().Verify(v => v.UpdateAsync(It.IsAny<PessoaViewModel>()), Times.Once);
            var ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact(DisplayName = "Deve falhar atualizar pessoa")]
        [Trait("Cadastro", "PessoaController")]
        public async Task Add_deve_falhar_atualizar()
        {
            //Arrange
            var pessoa = new PessoaViewModel()
            {
                Id = Guid.NewGuid(),
                Nome = ""
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification("Favor informar o Nome.")
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.HasNotification()).Returns(true);
            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.GetNotifications()).Returns(notifications);

            //Act
            var result = await _controller.Update(pessoa);

            //Assert
            _mocker.GetMock<IPessoaAppService>().Verify(v => v.UpdateAsync(It.IsAny<PessoaViewModel>()), Times.Once);
            _mocker.GetMock<DomainNotificationHandler>().Verify(e => e.HasNotification(), Times.Once);
            _mocker.GetMock<DomainNotificationHandler>().Verify(e => e.GetNotifications(), Times.Once);
            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }

    }
}
