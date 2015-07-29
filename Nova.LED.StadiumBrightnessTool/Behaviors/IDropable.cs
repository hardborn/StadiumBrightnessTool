using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    interface IDropable
    {
        Type DataType { get; }
        void Drop(object data, Point position);
    }
}
