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
    public class LEDBoxViewModel:BindableBase
    {

        private LEDBox _LEDBox;

        public LEDBoxViewModel(LEDBox box)
        {
            _LEDBox = box;
            SelectBoxCommand = new DelegateCommand<object>(SelectBox);
        }


        public const string IndexLocationPropertyName = "IndexLocation";

        private string _indexLocation =string.Empty;

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

        private void SelectBox(object obj)
        {

        }

         
    }
}
