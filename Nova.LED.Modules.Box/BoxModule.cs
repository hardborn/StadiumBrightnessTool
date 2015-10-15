using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Events;
using Nova.LED.Infrastructure.Interfaces;
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
        //private M3LCTServiceProxy _M3Service;
        private IEventAggregator _eventAggregator;
        private ILEDBoxService _LEDBoxService;

        //[ImportingConstructor]
        //public BoxModule(M3LCTServiceProxy serviceProxy,ILEDBoxService boxService, IEventAggregator eventAggregator)
        //{
        //    _LEDBoxService = boxService;
        //    _eventAggregator = eventAggregator;
        //    //_M3Service = serviceProxy;
        //}

        public void Initialize()
        {
            //_M3Service.InitalizeServerProxy();
            //_M3Service.RegisterToServer();
        }
    }
}
