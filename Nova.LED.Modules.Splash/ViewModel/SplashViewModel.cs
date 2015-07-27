using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Nova.LED.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Modules.Splash.ViewModel
{
    [Export]
    public class SplashViewModel : BindableBase
    {
        private string _status;

        [ImportingConstructor]
        public SplashViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<MessageUpdateEvent>().Subscribe(e => UpdateMessage(e.Message));
        }

        public string Status
        {
            get { return _status; }
            set
            {
                //_status = value;
                SetProperty(ref _status, value);
            }
        }


        private void UpdateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Status = string.Concat(message, "...");
        }
    }
}
