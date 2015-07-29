using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    interface IDragable
    {
        Type DataType { get; }
        void Copy(object data);
    }
}
