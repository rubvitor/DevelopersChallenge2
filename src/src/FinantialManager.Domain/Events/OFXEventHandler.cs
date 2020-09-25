using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FinantialManager.Domain.Events
{
    public class OFXEventHandler :
        INotificationHandler<OFXRegisteredEvent>,
        INotificationHandler<OFXUpdatedEvent>,
        INotificationHandler<OFXRemovedEvent>
    {
        public Task Handle(OFXUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(OFXRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(OFXRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}