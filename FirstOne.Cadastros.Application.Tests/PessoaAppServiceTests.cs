using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Application.Tests
{
    public class PessoaAppServiceTests
    {
        private readonly IPessoaAppService _pessoaAppService;
        private readonly AutoMocker _mocker;

        public PessoaAppServiceTests()
        {
            _mocker = new AutoMocker();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            }).CreateMapper();

            var pessoaRepository = _mocker.GetMock<IPessoaRepository>();
            var mediatorHandler = _mocker.GetMock<IMediatorHandler>();

            _pessoaAppService = new PessoaAppService(pessoaRepository.Object, mapper, mediatorHandler.Object);
        }

        [Fact(DisplayName = "GetAll deve listar pessoas")]
        public void GetAll_listar_todas_pessoas()
        {
            //Arrange
            var pessoas = new List<Pessoa>()
            {
                new Pessoa(Guid.NewGuid(), "Anthue"),
                new Pessoa(Guid.NewGuid(), "Teste")
            };

            _mocker.GetMock<IPessoaRepository>().Setup(e => e.GetAll()).Returns(pessoas);

            //Act
            var result = _pessoaAppService.GetAll();

            //Assert
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.GetAll(), Times.Once);
            Assert.Equal(2, result.Count());

        }

        [Fact(DisplayName = "Add deve adicionar pessoa")]
        public async Task Add_deve_adicionar_pessoa()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = "Tester"
            };

            //Act 
            await _pessoaAppService.AddAsync(pessoaViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(dn =>
                      dn.PublishDomainNotification(It.IsAny<DomainNotification>()), Times.Never);
            _mocker.GetMock<IMediatorHandler>().Verify(v => 
                      v.SendCommand(It.IsAny<AddPessoaCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Add deve falhar adicionar pessoa")]
        public async Task Add_deve_falhar_adicionar_pessoa()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = ""
            };

            //Act 
            await _pessoaAppService.AddAsync(pessoaViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(e => 
                       e.PublishDomainNotification(It.Is<DomainNotification>(
                           dm => dm.Value == "Favor informar o Nome.")), Times.Once);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.SendCommand(It.IsAny<AddPessoaCommand>()), Times.Never);
        }

        [Fact(DisplayName = "Add deve atualizar pessoa")]
        public async Task Add_deve_atualizar_pessoa()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = "Tester"
            };

            //Act 
            await _pessoaAppService.UpdateAsync(pessoaViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.PublishDomainNotification(It.IsAny<DomainNotification>()), Times.Never);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.SendCommand(It.IsAny<UpdatePessoaCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Add deve falhar atualizar pessoa validator nome")]
        public async Task Add_deve_falhar_atualizar_pessoa_validator_nome()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = ""
            };

            //Act 
            await _pessoaAppService.UpdateAsync(pessoaViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.PublishDomainNotification(It.Is<DomainNotification>(dn => 
                            dn.Value == "Favor informar o Nome.")), Times.Once);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.SendCommand(It.IsAny<UpdatePessoaCommand>()), Times.Never);
        }

        [Fact(DisplayName = "Add deve falhar atualizar pessoa validator id")]
        public async Task Add_deve_falhar_atualizar_pessoa_validator_id()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.Empty,
                Nome = "Tester"
            };

            //Act 
            await _pessoaAppService.UpdateAsync(pessoaViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.PublishDomainNotification(It.Is<DomainNotification>(dn =>
                            dn.Value == "Favor informar o Id.")), Times.Once);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                       e.SendCommand(It.IsAny<UpdatePessoaCommand>()), Times.Never);
        }
    }
}
