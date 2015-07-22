using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.LED.StadiumBrightnessTool.Model
{
    public class LEDBoxGroup
    {
        private string _id;
        private string _indexLocation;
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


        public List<LEDBox> Boxes
        {
            get { return _boxes; }
            set { _boxes = value; }
        }
    }
}
