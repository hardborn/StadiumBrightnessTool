using Microsoft.Practices.Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Modules.MockService
{
    [Export]
    public class MockModule:IModule
    {
        public void Initialize()
        {
            
        }
    }
}
