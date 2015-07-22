using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.Infrastructure.Models
{
    public class BrightnessController
    {
        private string _id;
        private int _brightnessValue;
        private byte _RGBBlue;
        private byte _RGBRed;
        private byte _RGBGreen;

        public string ID
        {
            get
            {
                return _id;
            }
        }

        public int BrightnessValue
        {
            get { return _brightnessValue; }
            set { _brightnessValue = value; }
        }

        public byte RGBBlue
        {
            get { return _RGBBlue; }
            set { _RGBBlue = value; }
        }

        public byte RGBRed
        {
            get { return _RGBRed; }
            set { _RGBRed = value; }
        }

        public byte RGBGreen
        {
            get { return _RGBGreen; }
            set { _RGBGreen = value; }
        }
    }
}
