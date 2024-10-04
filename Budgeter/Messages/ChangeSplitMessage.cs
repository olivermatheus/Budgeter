using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Budgeter.Messages;

public class ChangeSplitMessage1 : ValueChangedMessage<Split>
{
    public ChangeSplitMessage1(Split value) : base(value) {}
}
