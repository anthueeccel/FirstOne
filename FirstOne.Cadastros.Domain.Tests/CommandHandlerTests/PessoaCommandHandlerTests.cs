using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
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
    public class PessoaCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly PessoaCommandHandler _commandHanler;

        public PessoaCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _commandHanler = _mocker.CreateInstance<PessoaCommandHandler>();
        }

        [Fact(DisplayName = "PessoaCommandHanler Add")]
        public async Task deve_retornar_true()
        {
            //Arrange
            var addPessoaCommand = new AddPessoaCommand("Teste Pessoa");

            _mocker.GetMock<IPessoaRepository>()
               .Setup(e => e.UnitOfWork)
               .Returns(_mocker.GetMock<IUnitOfWork>().Object);

            //Act
            var result = await _commandHanler.Handle(addPessoaCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Add(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler Update")]
        public async Task deve_atualizar()
        {
            //Arrange
            var updatePessoaCommand = new UpdatePessoaCommand(Guid.NewGuid(), "Teste");

            _mocker.GetMock<IPessoaRepository>()
               .Setup(e => e.UnitOfWork)
               .Returns(_mocker.GetMock<IUnitOfWork>().Object);

            //Act
            var result = await _commandHanler.Handle(updatePessoaCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Update(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler Remove")]
        public async Task deve_remover()
        {
            //Arrange
            var removePessoaCommand = new RemovePessoaCommand(Guid.NewGuid());

            _mocker.GetMock<IPessoaRepository>()
                .Setup(e => e.UnitOfWork)
                .Returns(_mocker.GetMock<IUnitOfWork>().Object);

            //Act
            var result = await _commandHanler.Handle(removePessoaCommand, CancellationToken.None);

            //Assert
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Remove(It.IsAny<Guid>()), Times.Once);
        }
    }
}
