using Nova.Equipment.Protocol.TGProtocol;
using Nova.IO.Port;
using Nova.LCT.GigabitSystem.Common;
using Nova.LED.Infrastructure.Interfaces;
using Nova.Message.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Modules.Box.Services
{
    public class BrightnessService : IBrightnessService
    {
        private const char CommonProtocalTagSeperate = '#';
        private const string FrmTag = "DispalyAdjustment";
        private readonly string ReadGlobalBrightTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "ReadGlobalBrightness");
        private readonly string ReadRGBRedTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "ReadRedBrightness");
        private readonly string ReadRGBGreenTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "ReadGreenBrightness");
        private readonly string ReadRGBBlueTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "ReadBlueBrightness");

        private readonly string SendRedBrightTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "SetRedBrightness");
        private readonly string SendGlobalBrightTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "SetGlobalBrightness");
        private readonly string SendGreenBrightTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "SetGreenBrightness");
        private readonly string SendBlueBrightTag = string.Format("{0}{1}{2}", FrmTag, CommonProtocalTagSeperate, "SetBlueBrightness");


        private M3LCTServiceProxy _LCTService;
        private ILEDBoxService _boxService;

        [ImportingConstructor]
        public BrightnessService(M3LCTServiceProxy serviceProxy, ILEDBoxService boxService)
        {
            _LCTService = serviceProxy;
            _boxService = boxService;
        }


        public Task<int> GetBrightnessAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex)
        {
            var tcs = new TaskCompletionSource<int>();
            var requestData = TGProtocolParser.ReadGlobalBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    ReadGlobalBrightTag,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        int value;
                                                                        TGProtocolParser.ParseGlobalBrightness(e.Request as PackageRequestReadData, out value);
                                                                        tcs.SetResult(value);
                                                                    }));
            _LCTService.SendRequestReadData(requestData);

            return tcs.Task;
        }


        public Task<byte> GetRGBRedAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            var requestData = TGProtocolParser.ReadRedBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    ReadRGBRedTag,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        int value;
                                                                        TGProtocolParser.ParseRedBrightness(e.Request as PackageRequestReadData, out value);
                                                                        tcs.SetResult((byte)value);
                                                                    }));
            _LCTService.SendRequestReadData(requestData);

            return tcs.Task;
        }

        public Task<byte> GetRGBGreenAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            var requestData = TGProtocolParser.ReadGreenBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    ReadRGBGreenTag,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        int value;
                                                                        TGProtocolParser.ParseGreenBrightness(e.Request as PackageRequestReadData, out value);
                                                                        tcs.SetResult((byte)value);
                                                                    }));
            _LCTService.SendRequestReadData(requestData);

            return tcs.Task;
        }

        public Task<byte> GetRGBBlueAsync(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex)
        {
            var tcs = new TaskCompletionSource<byte>();
            var requestData = TGProtocolParser.ReadBlueBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    ReadRGBBlueTag,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        int value;
                                                                        TGProtocolParser.ParseBlueBrightness(e.Request as PackageRequestReadData, out value);
                                                                        tcs.SetResult((byte)value);
                                                                    }));
            _LCTService.SendRequestReadData(requestData);

            return tcs.Task;
        }

        public Task<bool> SetBrightness(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            var requestData = TGProtocolParser.SetGlobalBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    SendGlobalBrightTag,
                                                                    false,
                                                                    value,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        bool result = e.Request.PackResult == PackageResults.ok
                                                                                   && e.Request.CommResult == CommResults.ok
                                                                                   && (AckResults)e.Request.AckCode == AckResults.ok;
                                                                        tcs.SetResult(result);
                                                                    }));
            _LCTService.SendRequestWriteData(requestData);

            return tcs.Task;
        }

        public Task<bool> SetRGBRed(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            var requestData = TGProtocolParser.SetRedBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    SendRedBrightTag,
                                                                    false,
                                                                    value,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        bool result = e.Request.PackResult == PackageResults.ok
                                                                                   && e.Request.CommResult == CommResults.ok
                                                                                   && (AckResults)e.Request.AckCode == AckResults.ok;
                                                                        tcs.SetResult(result);
                                                                    }));
            _LCTService.SendRequestWriteData(requestData);

            return tcs.Task; 
        }

        public Task<bool> SetRGBGreen(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            var requestData = TGProtocolParser.SetGreenBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    SendGreenBrightTag,
                                                                    false,
                                                                    value,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        bool result = e.Request.PackResult == PackageResults.ok
                                                                                   && e.Request.CommResult == CommResults.ok
                                                                                   && (AckResults)e.Request.AckCode == AckResults.ok;
                                                                        tcs.SetResult(result);
                                                                    }));
            _LCTService.SendRequestWriteData(requestData);

            return tcs.Task; 
        }

        public Task<bool> SetRGBBlue(string COMIndex, byte senderIndex, byte portIndex, byte connectIndex, byte value)
        {
            var tcs = new TaskCompletionSource<bool>();
            var requestData = TGProtocolParser.SetBlueBrightness(COMIndex,
                                                                    senderIndex,
                                                                    portIndex,
                                                                    connectIndex,
                                                                    CommandTimeOut.SENDER_SIMPLYCOMMAND_TIMEOUT,
                                                                    SendBlueBrightTag,
                                                                    false,
                                                                    value,
                                                                    null,
                                                                    new CompletePackageRequestEventHandler((o, e) =>
                                                                    {
                                                                        bool result = e.Request.PackResult == PackageResults.ok
                                                                                   && e.Request.CommResult == CommResults.ok
                                                                                   && (AckResults)e.Request.AckCode == AckResults.ok;
                                                                        tcs.SetResult(result);
                                                                    }));
            _LCTService.SendRequestWriteData(requestData);

            return tcs.Task; 
        }
    }
}
