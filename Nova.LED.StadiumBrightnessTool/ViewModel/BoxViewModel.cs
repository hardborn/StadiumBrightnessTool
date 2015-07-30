using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    public class BoxViewModel : BindableBase
    {

        private LEDBox _LEDBox;

        public BoxViewModel(LEDBox box)
        {
            _LEDBox = box;
            SelectBoxCommand = new DelegateCommand<object>(SelectBox);
        }


        public const string IndexLocationPropertyName = "IndexLocation";

        private string _indexLocation = string.Empty;

        /// <summary>
        /// Sets and gets the IndexLocation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IndexLocation
        {
            get
            {
                return string.Format("{0}-S{1}-P{2}-{3}", _LEDBox.COMIndex, _LEDBox.SenderIndex, _LEDBox.PortIndex, _LEDBox.ConnectIndex);
            }

            //set
            //{
            //    SetProperty(ref _indexLocation, value);
            //}
        }

        public DelegateCommand<object> SelectBoxCommand { get; set; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        public string ID
        {
            get { return _LEDBox.ID; }
        }



        public double Width
        {
            get { return _LEDBox.Width; }
        }




        public double Height
        {
            get { return _LEDBox.Height; }
        }

        public ushort ConnectIndex
        {
            get
            {
                return _LEDBox.ConnectIndex;
            }
        }
        public byte PortIndex
        {
            get
            {
                return _LEDBox.PortIndex;
            }
        }

        public byte SenderIndex
        {
            get
            {
                return _LEDBox.SenderIndex;
            }
        }

        public ushort X
        {
            get
            {
                return _LEDBox.X;
            }
        }

        public ushort Y
        {
            get
            {
                return _LEDBox.Y;
            }
        }

        public ushort XInPort
        {
            get { return _LEDBox.XInPort; }
        }

        public ushort YInPort
        {
            get { return _LEDBox.YInPort; }
        }

        public string COMIndex
        {
            get { return _LEDBox.COMIndex; }
        }

        private void SelectBox(object obj)
        {
            IsSelected = !IsSelected;
        }


    }
}
