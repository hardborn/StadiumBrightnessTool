using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Nova.LED.Infrastructure.Models;
using Nova.LED.StadiumBrightnessTool.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    public class BoxGroupToolItemViewModel : BindableBase,IDragable
    {
        private LEDBoxGroup _boxGroup;

        public BoxGroupToolItemViewModel(LEDBoxGroup boxGroup)
        {
            _boxGroup = boxGroup;
        }


        private string _indexLocation = string.Empty;

        public string IndexLocation
        {
            get
            {
                return string.Format("{0}-S{1}-P{2}", _boxGroup.COMIndex, _boxGroup.SenderIndex, _boxGroup.PortIndex);
            }
            set
            {
                SetProperty(ref _indexLocation, value);
            }
        }

        private int _boxCount = 0;
        public int BoxCount
        {
            get
            {
                return _boxGroup.Boxes.Count;
            }
            set
            {
                SetProperty(ref _boxCount, value);
            }
        }


       
        public Type DataType
        {
            get { return typeof(LEDBoxGroup); }
        }

        public void Copy(object data)
        {
            //throw new NotImplementedException();
        }

        public object Data
        {
            get { return _boxGroup; }
        }

    }
}
