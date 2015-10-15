using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Infrastructure.Interfaces
{
    public interface ILEDBoxService
    {
        LEDBox GetBox(string COMIndex, int senderIndex, int portIndex, int connectIndex);
        Task<IList<LEDBoxGroup>> GetBoxGroupsAsync();

        event EventHandler BoxUpdated;


    }
}
