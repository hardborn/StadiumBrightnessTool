using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Infrastructure.Interfaces
{
    public interface IBrightnessService
    {
        Task<int> GetBrightnessAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex);
        Task<byte> GetRGBRedAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex);
        Task<byte> GetRGBGreenAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex);
        Task<byte> GetRGBBlueAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex);
        Task<bool> SetBrightness(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value);
        Task<bool> SetRGBRed(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value);
        Task<bool> SetRGBGreen(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value);
        Task<bool> SetRGBBlue(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value);
    }
}
