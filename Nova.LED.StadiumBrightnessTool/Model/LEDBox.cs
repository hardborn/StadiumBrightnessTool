using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Nova.LED.StadiumBrightnessTool.Model
{
    public class LEDBox
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }


        private double _height;

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private string _resolution;

        public string Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        private string _indexLocation;

        public string IndexLocation
        {
            get { return _indexLocation; }
            set { _indexLocation = value; }
        }

        private Point _pixelLocation;

        public Point PixelLocation
        {
            get { return _pixelLocation; }
            set { _pixelLocation = value; }
        }

    }
}
