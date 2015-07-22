using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Nova.LED.Infrastructure.Models
{
    public class LEDBox
    {
        private string _id;
        private ushort _connectIndex;
        private byte _portIndex;
        private byte _senderIndex;
        private ushort _x;
        private ushort _y;
        public ushort _xInPort;
        public ushort _yInPort;
        private double _width;
        private double _height;
        private string _COMIndex;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }



        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }




        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public ushort ConnectIndex
        {
            get
            {
                return this._connectIndex;
            }
            set
            {
                this._connectIndex = value;
            }
        }
        public byte PortIndex
        {
            get
            {
                return this._portIndex;
            }
            set
            {
                this._portIndex = value;
            }
        }

        public byte SenderIndex
        {
            get
            {
                return this._senderIndex;
            }
            set
            {
                this._senderIndex = value;
            }
        }

        public ushort X
        {
            get
            {
                return this._x;
            }
            set
            {
                this._x = value;
            }
        }

        public ushort Y
        {
            get
            {
                return this._y;
            }
            set
            {
                this._y = value;
            }
        }

        public ushort XInPort
        {
            get { return _xInPort; }
            set { _xInPort = value; }
        }

        public ushort YInPort
        {
            get { return _yInPort; }
            set { _yInPort = value; }
        }

        public string COMIndex
        {
            get { return _COMIndex; }
            set { COMIndex = value; }
        }
    }
}
