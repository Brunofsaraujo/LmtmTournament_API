using Flunt.Notifications;
using System;
using System.Linq;

namespace LmtmTournament.Domain.Entities
{
    public abstract class BaseEntity : Notifiable
    {
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected virtual void Validate() { }

        public TEntity GetEntityWithError<TEntity>(string errorMessage) where TEntity : BaseEntity, new()
        {
            AddNotification(nameof(TEntity), errorMessage);
            return (TEntity)this;
        }

        public string FirstOrDefaultErrorMessage
            => Notifications?.FirstOrDefault()?.Message;
    }
}
