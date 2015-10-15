using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Nova.LED.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool
{
    [Export]
    public class ShellViewModel:BindableBase
    {
        private string _status;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<MessageUpdateEvent>().Subscribe(e => UpdateStateMessage(e.Message));
        }

        public string Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private void UpdateStateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Status = message;
        }
    }
}
