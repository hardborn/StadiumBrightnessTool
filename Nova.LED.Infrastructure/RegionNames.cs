using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Infrastructure
{
    public static class RegionNames
    {
        public const string BoxGroupsRegion = "MainToolBarRegion";
        public const string MainRegion = "MainRegion";
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RegionNamesProxy
    {
        virtual public string BoxGroupsRegion
        {
            get { return RegionNames.BoxGroupsRegion; }
        }

        virtual public string MainRegion
        {
            get { return RegionNames.MainRegion; }
        }
    }
}
