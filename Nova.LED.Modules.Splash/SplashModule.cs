using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Modules.Splash.Events;
using Nova.LED.Modules.Splash.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Nova.LED.Modules.Splash
{
    [Export]
    public class SplashModule:IModule
    {

        private IEventAggregator _eventAggregator;
        private IShell _shell;
        private AutoResetEvent _waitForCreation;

        [ImportingConstructor]
        public SplashModule(IShell shell)
        {
            _shell = shell;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }
        public void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                _shell.Show();
                _eventAggregator.GetEvent<CloseSplashEvent>().Publish(new CloseSplashEvent());
            }));

            _waitForCreation = new AutoResetEvent(false);

            ThreadStart showSplash = () =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        var _splashView = ServiceLocator.Current.GetInstance<SplashView>();//SplashView必须在此线程创建
                        var dispatcher = _splashView.Dispatcher;
                        dispatcher.BeginInvoke((Action)_splashView.Show);
                        _eventAggregator.GetEvent<CloseSplashEvent>().Subscribe(
                          e => _splashView.Dispatcher.BeginInvoke((Action)_splashView.Close), ThreadOption.PublisherThread, true);

                        _waitForCreation.Set();
                    }));

                Dispatcher.Run();
            };
            var thread = new Thread(showSplash) { Name = "Splash Thread", IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            _waitForCreation.WaitOne();
        }
    }
}
