using Microsoft.Practices.Prism.Mvvm;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    [Export]
    public class BoxGroupViewModel :BindableBase 
    {
        private LEDBoxGroup _boxGroup;

        public BoxGroupViewModel(LEDBoxGroup boxGroup, ILEDBoxService boxService)
        {
            _boxGroup = boxGroup;
        }


        /// <summary>
        /// The <see cref="IndexLocation" /> property's name.
        /// </summary>
        public const string IndexLocationPropertyName = "IndexLocation";

        private string _indexLocation;

        /// <summary>
        /// Sets and gets the IndexLocation property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string IndexLocation
        {
            get
            {
                return string.Format("{0}-S{1}-P{2}", _boxGroup.COMIndex, _boxGroup.SenderIndex, _boxGroup.PortIndex);
            }

            //set
            //{
            //    SetProperty(ref _indexLocation, value);
            //}
        }

        /// <summary>
        /// The <see cref="BoxCount" /> property's name.
        /// </summary>
        public const string BoxCountPropertyName = "BoxCount";

        private int _boxCount = 0;

        /// <summary>
        /// Sets and gets the BoxCount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int BoxCount
        {
            get
            {
                return _boxGroup.Boxes.Count;
            }
            //set
            //{
            //    SetProperty(ref _boxCount, value);
            //}
        }



    }
}
