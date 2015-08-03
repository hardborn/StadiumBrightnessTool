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

        private LEDBox _LEDBox;
        private Timer _readDataTimer;
        private IBrightnessService _brightnessService;
        private IEventAggregator _eventAggregator;
        public BoxViewModel(LEDBox box)
        {
            _LEDBox = box;
            SelectBoxCommand = new DelegateCommand<object>(SelectBox);
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _brightnessService = ServiceLocator.Current.GetInstance<IBrightnessService>();
            _readDataTimer = new Timer(ReadData, null, 0, 1000 * 30);
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

        private int _currentBrightness;
        public int CurrentBrightness
        {
            get { return _currentBrightness; }
            set
            {
                SetProperty(ref _currentBrightness, value);
            }
        }


        private void SelectBox(object obj)
        {
            IsSelected = !IsSelected;
        }

        private async void ReadData(object state)
        {
            ReadDataAsync();
        }


        public async void ReadDataAsync()
        {
            CurrentBrightness = await _brightnessService.GetBrightnessAsync(COMIndex, SenderIndex, PortIndex, ConnectIndex);
            await _brightnessService.GetRGBRedAsync(COMIndex, SenderIndex, PortIndex, ConnectIndex);
            await _brightnessService.GetRGBGreenAsync(COMIndex, SenderIndex, PortIndex, ConnectIndex);
            await _brightnessService.GetRGBBlueAsync(COMIndex, SenderIndex, PortIndex, ConnectIndex);
        }

        public async Task<bool> SetBrightness(byte value)
        {
            bool result = await _brightnessService.SetBrightness(COMIndex, SenderIndex, PortIndex, ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust brightness successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust brightness failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBRed(byte value)
        {
            bool result = await _brightnessService.SetRGBRed(COMIndex, SenderIndex, PortIndex, ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(R) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(R) failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBGreen(byte value)
        {
            bool result = await _brightnessService.SetRGBGreen(COMIndex, SenderIndex, PortIndex, ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(G) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(G) failed", IndexLocation) });
            return result;
        }

        public async Task<bool> SetRGBBlue(byte value)
        {
            bool result = await _brightnessService.SetRGBBlue(COMIndex, SenderIndex, PortIndex, ConnectIndex, value);
            if (result)
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(B) successful", IndexLocation) });
            else
                _eventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = string.Format("[{0}] Adjust RGB(B) failed", IndexLocation) });
            return result;
        }



    }
}
