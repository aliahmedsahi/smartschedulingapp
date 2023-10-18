using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace SmartSchedulingApp.Notifications;

public class Notification : CreationAuditedEntity<Guid>
{
    public string Text { get; set; }
    public NotificationType NotificationType { get; set; }
    public Guid PersonId { get; set; }

    private Notification()
    {

    }
    public Notification(
        string text,
        NotificationType notificationType,
        Guid personId
        )
    {
        Text = text;
        NotificationType = notificationType;
        PersonId = personId;
    }
}

