using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using Nova.LED.Modules.Box;
using Nova.LED.StadiumBrightnessTool.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    [Export]
    public class BoxGroupViewModel : BindableBase
    {
        private LEDBoxGroup _boxGroup;


        public BoxGroupViewModel()
        {

        }

        public BoxGroupViewModel(LEDBoxGroup boxGroup)
        {
            _boxGroup = boxGroup;
            LEDBoxes = new ObservableCollection<BoxViewModel>();

            foreach (var item in _boxGroup.Boxes)
            {
                LEDBoxes.Add(new BoxViewModel(item));
            }
        }



        private ObservableCollection<BoxViewModel> _boxes;
        public ObservableCollection<BoxViewModel> LEDBoxes
        {
            get { return _boxes; }
            set
            {
                SetProperty(ref _boxes, value);
            }
        }


        public int BoxCount 
        {
            get { return _boxes.Count; }
           
        }

        public byte PortIndex
        {
            get
            {
                return _boxGroup.PortIndex;
            }
        }

        public byte SenderIndex
        {
            get
            {
                return _boxGroup.SenderIndex;
            }
        }

        public string COMIndex
        {
            get { return _boxGroup.COMIndex; }
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

        private double _elementPxPointX;
        public double ElementPxPointX
        {
            get { return _elementPxPointX; }
            set
            {
                SetProperty(ref _elementPxPointX, value);
            }
        }

        private double _elementPxPointY;
        public double ElementPxPointY
        {
            get { return _elementPxPointY; }
            set
            {
                SetProperty(ref _elementPxPointY, value);
            }
        }
       
    }
}
