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



        public BoxGroupViewModel(LEDBoxGroup boxGroup)
        {
            _boxGroup = boxGroup;
            _boxCount = _boxGroup.Boxes.Count;
            _COMIndex = _boxGroup.COMIndex;
            _senderIndex = (byte)(_boxGroup.SenderIndex + 1);
            _portIndex = (byte)(_boxGroup.PortIndex + 1);
            _indexLocation = GetIndexLocation(_boxGroup);

            LEDBoxes = new ObservableCollection<BoxViewModel>();

            var firstBox = _boxGroup.Boxes.FirstOrDefault(b => b.ConnectIndex == 0);
            if (firstBox == null)
            {
                return;
            }
            if (firstBox.X == 0)
            {
                _boxGroup.Boxes = _boxGroup.Boxes.OrderBy(b => b.ConnectIndex).ToList();
            }
            else
            {
                _boxGroup.Boxes = _boxGroup.Boxes.OrderByDescending(b => b.ConnectIndex).ToList();
            }

            foreach (var item in _boxGroup.Boxes)
            {
                LEDBoxes.Add(new BoxViewModel(item));
            }
        }


        public LEDBoxGroup BoxGroup { get { return _boxGroup; } }


        private ObservableCollection<BoxViewModel> _boxes;
        public ObservableCollection<BoxViewModel> LEDBoxes
        {
            get { return _boxes; }
            set
            {
                SetProperty(ref _boxes, value);
            }
        }


        private int _boxCount;
        public int BoxCount
        {
            get { return _boxCount; }
            set
            {
                SetProperty(ref _boxCount, value);
            }

        }


        private byte _portIndex;
        public byte PortIndex
        {
            get
            {
                return _portIndex;
            }
            set
            {
                SetProperty(ref _portIndex, value);
            }
        }


        private byte _senderIndex;
        public byte SenderIndex
        {
            get
            {
                return _senderIndex;
            }
            set
            {
                SetProperty(ref _senderIndex, value);
            }
        }


        private string _COMIndex;
        public string COMIndex
        {
            get { return _COMIndex; }
            set
            {
                SetProperty(ref _COMIndex, value);
            }
        }

        private string _indexLocation = string.Empty;
        public string IndexLocation
        {
            get
            {
                return _indexLocation;
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

        private string GetIndexLocation(LEDBoxGroup group)
        {
            return string.Format("{0}-S{1}-P{2}", group.COMIndex, group.SenderIndex + 1, group.PortIndex + 1);
        }

    }
}
