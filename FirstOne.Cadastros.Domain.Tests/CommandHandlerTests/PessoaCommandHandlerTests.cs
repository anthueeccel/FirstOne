using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using Moq;
using Moq.AutoMock;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstOne.Cadastros.Domain.Tests.CommandHandlerTests
{
    public class PessoaCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PessoaCommandHandler _commandHanler;

        public PessoaCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _commandHanler = _mocker.CreateInstance<PessoaCommandHandler>();
        }

        [Fact(DisplayName = "PessoaCommandHanler AddPessoaCommand")]
        public async Task deve_retornar_true()
        {
            //Arrange
            var addPessoaCommand = new AddPessoaCommand("Teste Pessoa");

            //Act
            var result = await _commandHanler.Handle(addPessoaCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Add(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler AddPessoaCommand")]
        public async Task deve_falhar_retornar_adicionar()
        {
            //Arrange
            var addPessoaCommand = new AddPessoaCommand("");

            //Act
            var result = await _commandHanler.Handle(addPessoaCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                    e.PublishDomainNotification(It.Is<DomainNotification>(dn =>
                           dn.Value == "Favor informar o Nome.")), Times.Once);
            _mocker.GetMock<IPessoaRepository>().Verify(v => v.Add(It.IsAny<Pessoa>()), Times.Never);
        }
    }
}
