using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.LED.Infrastructure.Interfaces
{
   public class LEDBoxGroupModelEventArgs: EventArgs
    {
       public LEDBoxGroupModelEventArgs(IList<LEDBoxGroup> groups)
       {
           Groups = groups;
       }

       public IList<LEDBoxGroup> Groups { get; set; }
    }
}
