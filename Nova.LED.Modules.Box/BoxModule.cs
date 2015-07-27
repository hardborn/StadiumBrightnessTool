using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Events;
using Nova.LED.Modules.Box.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nova.LED.Modules.Box
{
    [Export]
    public class BoxModule:IModule
    {
        private IEventAggregator _eventAggregator;
        public BoxModule()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        [Import]
        private M3LCTServiceProxy _M3Service;
        public void Initialize()
        {
            _M3Service.InitalizeServerProxy();
            Thread.Sleep(5000);
        }
    }
}
