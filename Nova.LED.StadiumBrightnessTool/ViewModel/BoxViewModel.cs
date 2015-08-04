using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Events;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    public class BoxViewModel : BindableBase
    {

        private LEDBox _box;
        private Timer _readDataTimer;
        private IBrightnessService _brightnessService;
        private IEventAggregator _eventAggregator;
        public BoxViewModel(LEDBox box)
        {
            _box = box;
            _COMIndex = _box.COMIndex;
            _senderIndex = (byte)(_box.SenderIndex + 1);
            _portIndex = (byte)(_box.PortIndex + 1);
            _connectIndex = (byte)(_box.ConnectIndex + 1);
            _width = _box.Width;
            _height = _box.Height;
            _x = _box.X;
            _y = _box.Y;
            _xInPort = _box.XInPort;
            _yInPort = _box.YInPort;
            _indexLocation = GetIndexLocation(_box);

            SelectBoxCommand = new DelegateCommand<object>(SelectBox);
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _brightnessService = ServiceLocator.Current.GetInstance<IBrightnessService>();
            _readDataTimer = new Timer(ReadData, null, 0, 1000 * 30);
        }


        public LEDBox Box { get { return _box; } }

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
                return _indexLocation;
            }
            set
            {
                SetProperty(ref _indexLocation, value);
            }
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

        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }


        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                SetProperty(ref _width, value);
            }
        }



        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                SetProperty(ref _height, value);
            }
        }


        private ushort _connectIndex;
        public ushort ConnectIndex
        {
            get
            {
                return _connectIndex;
            }
            set
            {
                SetProperty(ref _connectIndex, value);
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
                SetProperty(ref _senderIndex,value);
            }
        }


        private ushort _x;
        public ushort X
        {
            get
            {
                return _x;
            }
            set
            {
                SetProperty(ref _x, value);
            }
        }


        private ushort _y;
        public ushort Y
        {
            get
            {
                return _y;
            }
            set
            {
                SetProperty(ref _y,value);
            }
        }

        private ushort _xInPort;
        public ushort XInPort
        {
            get { return _xInPort; }
            set
            {
                SetProperty(ref _xInPort, value);
            }
        }

        private ushort _yInPort;
        public ushort YInPort
        {
            get { return _yInPort; }
            set
            {
                SetProperty(ref _yInPort, value);
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

        private int _currentBrightness;
        public int CurrentBrightness
        {
            get { return _currentBrightness; }
            set
            {
                SetProperty(ref _currentBrightness, value);
            }
        }


        private string GetIndexLocation(LEDBox box)
        {
            return string.Format("{0}-S{1}-P{2}-{3}", box.COMIndex, box.SenderIndex + 1, box.PortIndex + 1, box.ConnectIndex+1);
        }

        private void SelectBox(object obj)
        {
            IsSelected = !IsSelected;
        }

        private async void ReadData(object state)
        {
            ReadDataAsync();
        }


        public async void RefreshBrightness()
        {
           CurrentBrightness = await _brightnessService.GetBrightnessAsync(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex);
        }


        public async void ReadDataAsync()
        {
            CurrentBrightness = await _brightnessService.GetBrightnessAsync(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex);
            await _brightnessService.GetRGBRedAsync(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex);
            await _brightnessService.GetRGBGreenAsync(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex);
            await _brightnessService.GetRGBBlueAsync(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex);
        }


        public async Task<bool> SetBrightness(byte value)
        {
            bool result = await _brightnessService.SetBrightness(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust brightness successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust brightness failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBRed(byte value)
        {
            bool result = await _brightnessService.SetRGBRed(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(R) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(R) failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBGreen(byte value)
        {
            bool result = await _brightnessService.SetRGBGreen(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(G) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(G) failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBBlue(byte value)
        {
            bool result = await _brightnessService.SetRGBBlue(_box.COMIndex, _box.SenderIndex, _box.PortIndex, _box.ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(B) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(B) failed", IndexLocation) });
            return result;
        }



    }
}
