using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Modules.MockService.Services
{
    [Export(typeof(ILEDBoxService))]
    public class MockBoxService : ILEDBoxService
    {

        private IList<LEDBoxGroup> _groups = new List<LEDBoxGroup>();

        public LEDBox GetBox(string COMIndex, int senderIndex, int portIndex, int connectIndex)
        {
            return null;
        }

        public IList<LEDBoxGroup> GetBoxGroups()
        {
            List<LEDBoxGroup> groups = new List<LEDBoxGroup>()
            {
                new LEDBoxGroup()
                {
                    COMIndex = "COM3",
                    SenderIndex = 0,
                    PortIndex = 0,
                    Boxes = new List<LEDBox>()
                    {
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 1,
                            XInPort = 0,
                            YInPort = 0,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 2,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        }
                    }
                },
                 new LEDBoxGroup()
                {
                    COMIndex = "COM3",
                    SenderIndex = 0,
                    PortIndex = 1,
                    Boxes = new List<LEDBox>()
                    {
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 1,
                            ConnectIndex= 3,
                            XInPort = 0,
                            YInPort = 0,
                            Height=48,
                            Width =48
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 1,
                            ConnectIndex= 2,
                            XInPort = 48,
                            YInPort = 48,
                            Height=48,
                            Width =48
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 1,
                            ConnectIndex= 1,
                            XInPort = 96,
                            YInPort = 96,
                            Height=48,
                            Width =48
                        }
                    }
                }
            };
            _groups = groups;
            return groups;
        }

        public event EventHandler<LEDBoxGroupModelEventArgs> Updated;
    }
}
