using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Nova.LED.Infrastructure;
using Nova.LED.Infrastructure.Behaviors;
using Nova.LED.Infrastructure.Events;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Modules.Box;
using Nova.LED.Modules.MockService;
using Nova.LED.Modules.Splash;
using Nova.LED.Modules.Splash.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nova.LED.StadiumBrightnessTool
{
    public class StadiumBrightnessBootstrapper : MefBootstrapper
    {

        private IEventAggregator EventAggregator
        {
            get { return this.Container.GetExportedValue<IEventAggregator>(); }
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<IShell>() as DependencyObject;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StadiumBrightnessBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RegionNames).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SplashModule).Assembly));
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BoxModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MockModule).Assembly));
        }


        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }

        protected override void ConfigureContainer()
        {

            base.ConfigureContainer();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
           
            //IModule boxModule = this.Container.GetExportedValue<BoxModule>();
            //boxModule.Initialize();
        }
    }
}
