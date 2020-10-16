using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.Entities;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
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

            //Act
            var result = await _commandHanler.Handle(addPessoaCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Add(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler Add - falhar sem nome")]
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

        [Fact(DisplayName = "PessoaCommandHanler Update")]
        public async Task deve_atualizar()
        {
            //Arrange
            var updatePessoaCommand = new UpdatePessoaCommand(Guid.NewGuid(), "Teste");

            //Act
            var result = await _commandHanler.Handle(updatePessoaCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Update(It.IsAny<Pessoa>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler Update - falhar sem nome")]
        public async Task deve_falhar_atualizar_sem_nome()
        {
            //Arrange
            var updatePessoaCommand = new UpdatePessoaCommand(Guid.NewGuid(), "");

            //Act
            var result = await _commandHanler.Handle(updatePessoaCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                    e.PublishDomainNotification(It.Is<DomainNotification>(dn =>
                           dn.Value == "Favor informar o Nome.")), Times.Once);
            _mocker.GetMock<IPessoaRepository>().Verify(v => v.Update(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact(DisplayName = "PessoaCommandHanler Update - falhar sem id")]
        public async Task deve_falhar_atualizar_sem_id()
        {
            //Arrange
            var updatePessoaCommand = new UpdatePessoaCommand(Guid.Empty, "Teste");

            //Act
            var result = await _commandHanler.Handle(updatePessoaCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                    e.PublishDomainNotification(It.Is<DomainNotification>(dn =>
                           dn.Value == "Favor informar o Id.")), Times.Once);
            _mocker.GetMock<IPessoaRepository>().Verify(v => v.Update(It.IsAny<Pessoa>()), Times.Never);
        }

        [Fact(DisplayName = "PessoaCommandHanler Remove")]
        public async Task deve_remover()
        {
            //Arrange
            var removePessoaCommand = new RemovePessoaCommand(Guid.NewGuid());

            //Act
            var result = await _commandHanler.Handle(removePessoaCommand, CancellationToken.None);

            //Assert
            Assert.True(result);
            _mocker.GetMock<IPessoaRepository>().Verify(e => e.Remove(It.IsAny<Guid>()), Times.Once);
        }

        [Fact(DisplayName = "PessoaCommandHanler Remove = falhar sem id")]
        public async Task deve_falhar_remover_sem_id()
        {
            //Arrange
            var removePessoaCommand = new RemovePessoaCommand(Guid.Empty);

            //Act
            var result = await _commandHanler.Handle(removePessoaCommand, CancellationToken.None);

            //Assert
            Assert.False(result);
            _mocker.GetMock<IMediatorHandler>().Verify(e =>
                    e.PublishDomainNotification(It.Is<DomainNotification>(dn =>
                           dn.Value == "Favor informar o Id.")), Times.Once);
            _mocker.GetMock<IPessoaRepository>().Verify(v => v.Remove(It.IsAny<Guid>()), Times.Never);
        }

    }
}
