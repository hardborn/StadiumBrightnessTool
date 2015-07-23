using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nova.LED.StadiumBrightnessTool.Controls
{
    public class LEDBoxGroupTool : ItemsControl
    {

        // Creates or identifies the element that is used to display the given item.        
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new LEDBoxGroupToolItem();
        }

        // Determines if the specified item is (or is eligible to be) its own container.        
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is LEDBoxGroupToolItem);
        }
    }

}
