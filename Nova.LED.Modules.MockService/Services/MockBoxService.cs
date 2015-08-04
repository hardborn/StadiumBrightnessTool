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

        public MockBoxService()
        {

        }

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


        public Task<IList<LEDBoxGroup>> GetBoxGroupsAsync()
        {
            var tcs = new TaskCompletionSource<IList<LEDBoxGroup>>();

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
                            ConnectIndex= 0,
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
                            ConnectIndex= 1,
                            XInPort = 32,
                            YInPort = 32,
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
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 3,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 4,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 5,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        ////------------------------------------------------------------------
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 6,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 7,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 8,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 9,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 10,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 11,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 12,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 13,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 14,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 15,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 16,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 17,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 18,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 19,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 20,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 21,
                            XInPort = 32,
                            YInPort =32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 22,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 23,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 24,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 25,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 26,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 27,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 28,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 29,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 30,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 31,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 32,
                            XInPort = 32,
                            YInPort = 32,
                            Height=32,
                            Width =32
                        },
                           new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 33,
                            XInPort = 64,
                            YInPort = 64,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 34,
                            XInPort = 96,
                            YInPort = 96,
                            Height=32,
                            Width =32
                        },
                        new LEDBox()
                        {
                            COMIndex = "COM3",
                            SenderIndex = 0,
                            PortIndex = 0,
                            ConnectIndex= 35,
                            XInPort = 128,
                            YInPort = 128,
                            Height=32,
                            Width =32
                        }
                        /////////----------------------------------------------------------------------

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

            tcs.SetResult(_groups);
            return tcs.Task;
        }
    }
}
