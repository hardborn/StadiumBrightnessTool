using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
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
    public class LocationProfileViewModel : BindableBase
    {
        private ILEDBoxService _LEDBoxService;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<BoxGroupViewModel> _boxGroups;

        public ObservableCollection<BoxGroupViewModel> BoxGroups
        {
            get { return _boxGroups; }
            set { SetProperty(ref _boxGroups, value); }
        }


        private BoxGroupViewModel _currentBoxGroup;
        public BoxGroupViewModel CurrentBoxGroup { 
            get { return _currentBoxGroup; }
            set
            {
                SetProperty(ref _currentBoxGroup, value);
            }
        }

        public LocationProfileViewModel()
        {
            _LEDBoxService = ServiceLocator.Current.GetInstance<ILEDBoxService>();
            BoxGroups = new ObservableCollection<BoxGroupViewModel>();
            PopulateBoxGroups();
        }

        private void PopulateBoxGroups()
        {
            BoxGroupViewModel boxGroup;
            foreach (LEDBoxGroup groupItem in this._LEDBoxService.GetBoxGroups())
            {
                boxGroup = new BoxGroupViewModel(groupItem);
                this.BoxGroups.Add(boxGroup);
            }
        }

    }
}
