using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Infrastructure.Models;
using Nova.LED.StadiumBrightnessTool.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LocationProfileViewModel : BindableBase, IDropable
    {
        private ILEDBoxService _LEDBoxService;
        private IEventAggregator _eventAggregator;
       


        public LocationProfileViewModel()
        {
            _LEDBoxService = ServiceLocator.Current.GetInstance<ILEDBoxService>();
            BoxGroups = new ObservableCollection<BoxGroupViewModel>();
            BoxGroupsToolItem = new ObservableCollection<BoxGroupToolItemViewModel>();
            PopulateBoxGroups();
            SaveConfigurationCommand = new DelegateCommand(SaveConfiguration);
        }


        private ObservableCollection<BoxGroupViewModel> _boxGroups;        
        public ObservableCollection<BoxGroupViewModel> BoxGroups
        {
            get { return _boxGroups; }
            set { SetProperty(ref _boxGroups, value); }
        }

        private ObservableCollection<BoxGroupToolItemViewModel> _boxGroupsToolItem;
        public ObservableCollection<BoxGroupToolItemViewModel> BoxGroupsToolItem
        {
            get { return _boxGroupsToolItem; }
            set { SetProperty(ref _boxGroupsToolItem, value); }
        }


        private BoxGroupToolItemViewModel _currentBoxGroupToolItem;
        public BoxGroupToolItemViewModel CurrentBoxGroupToolItem
        {
            get { return _currentBoxGroupToolItem; }
            set
            {
                SetProperty(ref _currentBoxGroupToolItem, value);
            }
        }

        public DelegateCommand SaveConfigurationCommand { get; set; }

        private void PopulateBoxGroups()
        {
            BoxGroupToolItemViewModel boxGroup;
            foreach (LEDBoxGroup groupItem in this._LEDBoxService.GetBoxGroups())
            {
                boxGroup = new BoxGroupToolItemViewModel(groupItem);
                this.BoxGroupsToolItem.Add(boxGroup);
            }
        }

        private void SaveConfiguration()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "配置文件 (*.xml)|*.xml";
            dialog.DefaultExt = ".xml";
            dialog.Title = "保存配置文件";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                SaveComponentXml(filename);
            }
        }

        private void SaveComponentXml(string filePath)
        {

            if (BoxGroups != null)
            {
                XElement root = new XElement("BoxGroupConfigurationInfo");

                foreach (var group in BoxGroups)
                {
                    XElement boxGroupElement = new XElement("BoxGroup");
                    XElement boxGroupInfoElement = new XElement("BoxGroupInfo", 
                        new XAttribute("IndexLocation", group.IndexLocation),
                        new XAttribute("ElementPxPointX", group.ElementPxPointX),
                        new XAttribute("ElementPxPointY", group.ElementPxPointY));
                    boxGroupElement.Add(boxGroupInfoElement);

                    XElement boxesElement = new XElement("Boxes");
                    if (group.LEDBoxes != null)
                    {
                        foreach (var box in group.LEDBoxes)
                        {
                            boxesElement.Add(new XElement("Box",
                                new XAttribute("COMIndex", box.COMIndex),
                                new XAttribute("SenderIndex", box.SenderIndex),
                                new XAttribute("PortIndex", box.PortIndex),
                                new XAttribute("ConnectIndex", box.ConnectIndex),
                                new XAttribute("Width", box.Width),
                                new XAttribute("Height", box.Height),
                                new XAttribute("XInPort", box.XInPort),
                                new XAttribute("YInPort", box.YInPort),
                                new XAttribute("X", box.X),
                                new XAttribute("Y", box.Y)));
                        }
                    }
                    boxGroupElement.Add(boxesElement);

                    root.Add(boxGroupElement);
                }

                root.Save(filePath);
            }
        }

        #region IDragable

        public Type DataType
        {
            get { return typeof(LEDBoxGroup); }
        }


        public void Drop(object data, System.Windows.Point position)
        {
            var boxGroup = data as LEDBoxGroup;
            if (boxGroup != null)
            {
                var boxGroupViewModel = new BoxGroupViewModel(boxGroup);
                
                if (!BoxGroups.Any(g => g.IndexLocation == boxGroupViewModel.IndexLocation))
                {
                    boxGroupViewModel.ElementPxPointX = position.X;
                    boxGroupViewModel.ElementPxPointY = position.Y;
                    BoxGroups.Add(boxGroupViewModel);
                }               
            }
        }

        #endregion
    }
}
