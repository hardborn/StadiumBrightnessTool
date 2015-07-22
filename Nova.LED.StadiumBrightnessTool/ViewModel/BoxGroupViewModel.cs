using Microsoft.Practices.Prism.Mvvm;
using Nova.LED.StadiumBrightnessTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    public class BoxGroupViewModel :BindableBase 
    {
        private LEDBoxGroup _boxGroup;

        public BoxGroupViewModel(LEDBoxGroup boxGroup)
        {

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
                return _indexLocation;
            }

            set
            {
                SetProperty(ref _indexLocation, value);
            }
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
                return _boxCount;
            }

            set
            {
                SetProperty(ref _boxCount, value);
            }
        }



    }
}
