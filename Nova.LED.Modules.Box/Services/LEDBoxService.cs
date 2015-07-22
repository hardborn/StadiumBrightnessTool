using Nova.LCT.Message.Client;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using Nova.LED.Modules.Box.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Nova.LED.Modules.Box.Services
{
    public class LEDBoxService : ILEDBoxService
    {

        private IList<LEDBoxGroup> _groups = new List<LEDBoxGroup>();
        private List<LEDBox> _boxes = new List<LEDBox>();

        private M3LCTServiceProxy _LCTService;

        [ImportingConstructor]
        public LEDBoxService(M3LCTServiceProxy serviceProxy)
        {
            _LCTService = serviceProxy;
            InitializeLEDBox();
        }

        private void InitializeLEDBox()
        {
            _LCTService.ReadCOMHWBaseInfoAsync((info, o) =>
                {
                    if (info == null || info.AllInfo == null || info.AllInfo.AllInfoDict == null)
                    {
                        return;
                    }

                    var currentCOMAndHWBaseInfo = info.AllInfo.AllInfoDict.ElementAt(0);
                    var currentHWBaseInfo = currentCOMAndHWBaseInfo.Value;
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
                            box.SenderIndex = receivingCardItem.SenderIndex;
                            box.PortIndex = receivingCardItem.PortIndex;
                            box.ConnectIndex = receivingCardItem.ConnectIndex;
                            _boxes.Add(box);
                        }
                    }
                    var boxGroupList = _boxes.GroupBy(x => new { x.SenderIndex, x.PortIndex })
                                             .Select(y => new LEDBoxGroup()
                                             {
                                                 SenderIndex = y.Key.SenderIndex,
                                                 PortIndex = y.Key.PortIndex,
                                                 Boxes = y.ToList()
                                             });
                    _groups = new List<LEDBoxGroup>(boxGroupList);
                });
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

        public IList<LEDBoxGroup> GetBoxGroups()
        {
            return _groups;
        }


        public event EventHandler<LEDBoxGroupModelEventArgs> Updated;
    }
}
