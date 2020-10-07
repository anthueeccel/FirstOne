using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
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

            _pessoaAppService = new PessoaAppService(pessoaRepository.Object, mapper);
        }

        [Fact(DisplayName = "GetAll_listar_todas_pessoas")]
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


        [Fact(DisplayName = "AddAsync_deve_adicionar_pessoa")]
        public void AddAsync_deve_adicionar_pessoa()
        {
            //Arrange 
            var pessoaViewModel = new PessoaViewModel
            {
                Id = Guid.NewGuid(),
                Nome = "Tester"
            };

            //Act 
            var result = _pessoaAppService.AddAsync(pessoaViewModel);

            //Assert
            Assert.True(result.IsValid);
        }
    }
}
