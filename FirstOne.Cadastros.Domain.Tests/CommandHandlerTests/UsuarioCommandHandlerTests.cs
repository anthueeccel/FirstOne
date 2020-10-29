using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands.Usuario;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.CommandHandlerTests
{
    public class UsuarioCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly UsuarioCommandHandler _commandHanler;

        public UsuarioCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _commandHanler = _mocker.CreateInstance<UsuarioCommandHandler>();
        }

        [Fact(DisplayName = "UsuarioCommandHanler Add")]
        public async Task deve_retornar_true()
        {
            //Arrange
            var addUsuarioCommand = new AddUsuarioCommand("Teste Usuario", "1234", Guid.NewGuid());

            _mocker.GetMock<IUsuarioRepository>()
               .Setup(e => e.UnitOfWork)
               .Returns(_mocker.GetMock<IUnitOfWork>().Object);

            //Act
            var result = await _commandHanler.Handle(addUsuarioCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IUsuarioRepository>().Verify(e => e.Add(It.IsAny<Usuario>()), Times.Once);
        }        
    }
}
