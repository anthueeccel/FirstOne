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

            //Act
            var result = await _commandHanler.Handle(addUsuarioCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IUsuarioRepository>().Verify(e => e.Add(It.IsAny<Usuario>()), Times.Once);
        }

        //[Fact(DisplayName = "PessoaCommandHanler Update")]
        //public async Task deve_atualizar()
        //{
        //    //Arrange
        //    var updatePessoaCommand = new UpdatePessoaCommand(Guid.NewGuid(), "Teste");

        //    //Act
        //    var result = await _commandHanler.Handle(updatePessoaCommand, CancellationToken.None);

        //    //Assert
        //    _mocker.GetMock<IPessoaRepository>().Verify(e => e.Update(It.IsAny<Pessoa>()), Times.Once);
        //}

        //[Fact(DisplayName = "PessoaCommandHanler Remove")]
        //public async Task deve_remover()
        //{
        //    //Arrange
        //    var removePessoaCommand = new RemovePessoaCommand(Guid.NewGuid());

        //    //Act
        //    var result = await _commandHanler.Handle(removePessoaCommand, CancellationToken.None);

        //    //Assert
        //    _mocker.GetMock<IPessoaRepository>().Verify(e => e.Remove(It.IsAny<Guid>()), Times.Once);
        //}
    }
}
