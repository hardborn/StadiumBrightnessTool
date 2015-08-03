using Microsoft.Practices.Prism.PubSubEvents;
using Nova.LCT.Message.Client;
using Nova.LED.Infrastructure.Events;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using Nova.LED.Modules.Box.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Nova.LED.Modules.Box.Services
{
    [Export(typeof(ILEDBoxService))]
    [PartCreationPolicy(CreationPolicy.Shared)]

    public class LEDBoxService : ILEDBoxService
    {

        private IList<LEDBoxGroup> _groups = new List<LEDBoxGroup>();
        private List<LEDBox> _boxes = new List<LEDBox>();

        private M3LCTServiceProxy _LCTService;
        private IEventAggregator _eventAggregator;

       // private AutoResetEvent _waitForReadData;

        [ImportingConstructor]
        public LEDBoxService(M3LCTServiceProxy serviceProxy,IEventAggregator eventAggregator)
        {
            _LCTService = serviceProxy;
            _eventAggregator = eventAggregator;
            //InitializeLEDBox();
        }

        public void InitializeLEDBox()
        {
            GetBoxGroupsAsync();
        }

        private string GetIndexLocation(string p1, byte p2, byte p3, ushort p4)
        {
            return string.Format("{0}-{1}-{2}-{3}", p1, p2, p3, p4);
        }

        private string GetResolution(ushort p1, ushort p2)
        {
            return string.Format("{0}*{1}", p1, p2);
        }

        public LEDBox GetBox(string COMIndex, int senderIndex, int portIndex, int connectIndex)
        {

            return _boxes.SingleOrDefault(b => b.COMIndex == COMIndex
                                            && b.SenderIndex == senderIndex
                                            && b.PortIndex == portIndex
                                            && b.ConnectIndex == connectIndex);
        }

        public Task<IList<LEDBoxGroup>> GetBoxGroupsAsync()
        {
            var tcs = new TaskCompletionSource<IList<LEDBoxGroup>>();
            _LCTService.ReadCOMHWBaseInfoAsync((info, o) =>
            {
                if (info == null || info.AllInfo == null || info.AllInfo.AllInfoDict == null)
                {
                    //_waitForReadData.Set();
                    return;
                }

                //_eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent() { Message = "Get LED Boxes Data" });
                var currentCOMAndHWBaseInfo = info.AllInfo.AllInfoDict.ElementAt(0);
                var currentHWBaseInfo = currentCOMAndHWBaseInfo.Value;
                if (currentHWBaseInfo == null || currentHWBaseInfo.LEDDisplayInfoList == null)
                {
                    // _waitForReadData.Set();
                    return;
                }
                foreach (var item in currentHWBaseInfo.LEDDisplayInfoList)
                {
                    foreach (var receivingCardItem in item.GetAreaScanBdList(new System.Drawing.Rectangle(item.GetScreenPosition(), item.GetScreenSize())))
                    {
                        LEDBox box = new LEDBox();
                        box.Height = receivingCardItem.Height;
                        box.Width = receivingCardItem.Width;
                        box.X = receivingCardItem.X;
                        box.Y = receivingCardItem.Y;
                        box.XInPort = receivingCardItem.XInPort;
                        box.YInPort = receivingCardItem.YInPort;
                        box.COMIndex = currentCOMAndHWBaseInfo.Key;
                        box.SenderIndex = (byte)(receivingCardItem.SenderIndex+1);
                        box.PortIndex = (byte)(receivingCardItem.PortIndex+1);
                        box.ConnectIndex = (byte)(receivingCardItem.ConnectIndex+1);
                        _boxes.Add(box);
                    }
                }
                var boxGroupList = _boxes.GroupBy(x => new { x.COMIndex, x.SenderIndex, x.PortIndex })
                                         .Select(y => new LEDBoxGroup()
                                         {
                                             COMIndex = y.Key.COMIndex,
                                             SenderIndex = y.Key.SenderIndex,
                                             PortIndex = y.Key.PortIndex,
                                             Boxes = y.ToList()
                                         });
                _groups = new List<LEDBoxGroup>(boxGroupList);
                tcs.SetResult(_groups);
                //  _waitForReadData.Set();
            });

            return tcs.Task;
        }


        public event EventHandler<LEDBoxGroupModelEventArgs> Updated;


    }
}
