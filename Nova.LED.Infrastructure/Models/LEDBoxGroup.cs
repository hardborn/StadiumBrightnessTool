using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.LED.Infrastructure.Models
{
    public class LEDBoxGroup
    {
        private string _id;
        private string _indexLocation;
        private string _COMIndex;
        private byte _senderIndex;
        private byte _portIndex;
        private List<LEDBox> _boxes;

        public LEDBoxGroup()
        {

        }

        public string ID
        {
            get { return _id; }
        }

        public string IndexLocation
        {
            get { return _indexLocation; }
            set { _indexLocation = value; }
        }


        public string COMIndex
        {
            get { return _COMIndex; }
            set
            {
                _COMIndex = value;
            }

        }
        public byte SenderIndex
        {
            get { return _senderIndex; }
            set { _senderIndex = value; }
        }

        public byte PortIndex
        {
            get { return _portIndex; }
            set { _portIndex = value; }
        }



        public List<LEDBox> Boxes
        {
            get { return _boxes; }
            set { _boxes = value; }
        }
    }
}
