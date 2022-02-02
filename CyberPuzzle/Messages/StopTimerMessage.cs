using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace CyberPuzzle.Messages
{
    public class StopTimerMessage : ValueChangedMessage<object>
    {
        public StopTimerMessage(object value) : base(value)
        {
        }
    }
}
