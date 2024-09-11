using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Budgeter.Messages;

public class DeleteSplitMessage : ValueChangedMessage<Split>
{
    public DeleteSplitMessage(Split value) : base(value) {}
}
