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


namespace FirstOne.Cadastros.Api.Tests.ControllerTests
{
    public class UsuarioControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _mocker = new AutoMocker();

            _controller = new UsuarioController(_mocker.GetMock<IUsuarioAppService>().Object,
                                               _mocker.GetMock<DomainNotificationHandler>().Object);
        }
    
        [Fact(DisplayName = "Deve falhar adicionar usuario")]
        [Trait("Cadastro", "UsuarioController")]
        public async Task Add_deve_falhar_adicionar()
        {
            var usuario = new UsuarioViewModel()
            {
                Email = "",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            var notifications = new List<DomainNotification>()
            {
                new DomainNotification(string.Format(ValidationMessages.RequiredField, "Email"))
            };

            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.HasNotification()).Returns(true);
            _mocker.GetMock<DomainNotificationHandler>().Setup(e => e.GetNotifications()).Returns(notifications);

            //Act
            var result = await _controller.Add(usuario);

            //Assert
            _mocker.GetMock<DomainNotificationHandler>().Verify(dn => dn.HasNotification(), Times.Once);
            _mocker.GetMock<IUsuarioAppService>().Verify(e => e.AddAsync(It.IsAny<UsuarioViewModel>()), Times.Once);

            var erro = result as UnprocessableEntityObjectResult;
            Assert.NotNull(erro);
            Assert.Equal(422, erro.StatusCode);
        }
    }
}
