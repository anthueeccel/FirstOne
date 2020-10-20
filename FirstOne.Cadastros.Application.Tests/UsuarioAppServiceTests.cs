using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Application.ViewModels;
using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Application.Tests
{
    public class UsuarioAppServiceTests
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly AutoMocker _mocker;

        public UsuarioAppServiceTests()
        {
            _mocker = new AutoMocker();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            }).CreateMapper();

            var usuarioRepository = _mocker.GetMock<IUsuarioRepository>();
            var mediatorHandler = _mocker.GetMock<IMediatorHandler>();

            _usuarioAppService = new UsuarioAppService(usuarioRepository.Object, mapper, mediatorHandler.Object);
        }

        //[Fact(DisplayName = "GetAll deve listar usuarios")]
        //public void GetAll_listar_todos_usuarios()
        //{
        //    //Arrange
        //    var usuarios = new List<Usuario>()
        //    {
        //        new Usuario(Guid.NewGuid(), "Anthue"),
        //        new Usuario(Guid.NewGuid(), "Teste")
        //    };

        //    _mocker.GetMock<IPessoaRepository>().Setup(e => e.GetAll()).Returns(usuarios);

        //    //Act
        //    var result = _usuarioAppService.GetAll();

        //    //Assert
        //    _mocker.GetMock<IPessoaRepository
        //        >().Verify(e => e.GetAll(), Times.Once);
        //    Assert.Equal(2, result.Count());

        //}

        [Fact(DisplayName = "Add deve adicionar usuário")]
        public async Task Add_deve_adicionar_usuario()
        {
            //Arrange 
            var usuarioViewModel = new UsuarioViewModel()
            {
                Email = "test@test.com",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            //Act 
            await _usuarioAppService.AddAsync(usuarioViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(dn =>
                      dn.PublishDomainNotification(It.IsAny<DomainNotification>()), Times.Never);
            _mocker.GetMock<IMediatorHandler>().Verify(v =>
                      v.SendCommand(It.IsAny<AddUsuarioCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Add deve falhar adicionar usuário")]
        public async Task Add_deve_falhar_adicionar_usuario_sem_email()
        {
            //Arrange 
            var usuarioViewModel = new UsuarioViewModel()
            {
                Email = "",
                Senha = "1234",
                PessoaId = Guid.NewGuid()
            };

            //Act 
            await _usuarioAppService.AddAsync(usuarioViewModel);

            //Assert
            _mocker.GetMock<IMediatorHandler>().Verify(dn =>
                      dn.PublishDomainNotification(It.Is<DomainNotification>(
                          e => e.Value == string.Format(ValidationMessages.RequiredField, "Email"))), Times.Once);
            _mocker.GetMock<IMediatorHandler>().Verify(v =>
                      v.SendCommand(It.IsAny<AddUsuarioCommand>()), Times.Never);
        }
    }
}
