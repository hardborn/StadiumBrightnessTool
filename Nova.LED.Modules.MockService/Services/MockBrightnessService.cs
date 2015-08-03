using Nova.LED.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Modules.MockService.Services
{
    [Export(typeof(IBrightnessService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MockBrightnessService:IBrightnessService
    {
        public Task<int> GetBrightnessAsync(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex)
        {
            var tcs = new TaskCompletionSource<int>();            
            tcs.SetResult(77);
            return tcs.Task;
        }

        public Task<byte> GetRGBRedAsync(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            tcs.SetResult(77);
            return tcs.Task;
        }

        public Task<byte> GetRGBGreenAsync(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            tcs.SetResult(77);
            return tcs.Task;
        }

        public Task<byte> GetRGBBlueAsync(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            tcs.SetResult(77);
            return tcs.Task;
        }

        public Task<bool> SetBrightness(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }

        public Task<bool> SetRGBRed(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }

        public Task<bool> SetRGBGreen(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }

        public Task<bool> SetRGBBlue(string COMIndex, byte senderIndex, byte portIndex, ushort connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
