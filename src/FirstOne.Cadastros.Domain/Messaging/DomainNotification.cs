using MediatR;

namespace FirstOne.Cadastros.Domain.Messaging
{
    public class DomainNotification : INotification
    {
        public string Value { get; }

        public DomainNotification(string value)
        {
            Value = value;
        }
    }
}
