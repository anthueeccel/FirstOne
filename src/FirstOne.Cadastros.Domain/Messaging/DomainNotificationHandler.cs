using FirstOne.Cadastros.Domain.Mediator;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FirstOne.Cadastros.Domain.Messaging
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        internal List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotification()
        {
            return _notifications.Count > 0;
        }
    }
}
