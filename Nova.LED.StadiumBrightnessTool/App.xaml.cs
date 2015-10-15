using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Nova.LED.StadiumBrightnessTool
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }


        private static void RunInDebugMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            StadiumBrightnessBootstrapper bootstrapper = new StadiumBrightnessBootstrapper();
            bootstrapper.Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                StadiumBrightnessBootstrapper bootstrapper = new StadiumBrightnessBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                // HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
