using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using Nova.LED.Infrastructure;
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
using System.Windows.Input;
using System.Xml.Linq;

namespace Nova.LED.StadiumBrightnessTool.ViewModel
{
    [Export(typeof(LocationProfileViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LocationProfileViewModel : BindableBase, IDropable
    {
        private ILEDBoxService _LEDBoxService;
        private readonly IRegionManager _regionManager;


        [ImportingConstructor]
        public LocationProfileViewModel(ILEDBoxService boxService, IRegionManager regionManager)
        {
            _LEDBoxService = boxService;
            _regionManager = regionManager;
            BoxGroups = new ObservableCollection<BoxGroupViewModel>();
            BoxGroupsToolItem = new ObservableCollection<BoxGroupToolItemViewModel>();
            PopulateBoxGroups();
            SaveConfigurationCommand = new DelegateCommand(SaveConfiguration);
            LoadConfigurationCommand = new DelegateCommand(LoadConfiguration);
            SetBrightnessCommand = new DelegateCommand<object>(SetBrightness);
            ZoomCommand = new DelegateCommand<MouseWheelEventArgs>(ZoomCanvas);
            SelectAllCommand = new DelegateCommand(SelectAll);
        }

        private void SelectAll()
        {
            foreach (var group in BoxGroups)
            {
                foreach (var box in group.LEDBoxes)
                {
                    box.IsSelected = true;
                }
            }
        }

        private void ZoomCanvas(MouseWheelEventArgs obj)
        {

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

        private byte _adjustmentBrightness;
        public byte AdjustmentBrightness
        {
            get { return _adjustmentBrightness; }
            set
            {
                if (SetProperty(ref _adjustmentBrightness, value))
                {
                    SetBrightness(value);
                }
            }
        }

        private byte _adjustmentRGBRed;
        public byte AdjustmentRGBRed
        {
            get { return _adjustmentRGBRed; }
            set
            {
                if(SetProperty(ref _adjustmentRGBRed, value))
                {
                    SetRGBRed(value);
                }
            }
        }

        private byte _adjustmentRGBGreen;
        public byte AdjustmentRGBGreen
        {
            get { return _adjustmentRGBGreen; }
            set
            {
                if(SetProperty(ref _adjustmentRGBGreen, value))
                {
                    SetRGBGreen(value);
                }
            }
        }

        private byte _adjustmentRGBBlue;
        public byte AdjustmentRGBBlue
        {
            get { return _adjustmentRGBBlue; }
            set
            {
               if( SetProperty(ref _adjustmentRGBBlue, value))
               {
                   SetRGBBlue(value);
               }
            }
        }

        public DelegateCommand SaveConfigurationCommand { get; set; }

        public DelegateCommand LoadConfigurationCommand { get; set; }

        public DelegateCommand<object> SetBrightnessCommand { get; set; }

        public DelegateCommand<MouseWheelEventArgs> ZoomCommand { get; set; }

        public DelegateCommand SelectAllCommand { get; set; }

        private async void PopulateBoxGroups()
        {
            BoxGroupToolItemViewModel boxGroup;
            var boxGroups = await _LEDBoxService.GetBoxGroupsAsync();
            foreach (LEDBoxGroup groupItem in boxGroups)
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
                        new XAttribute("COMIndex", group.COMIndex),
                        new XAttribute("SenderIndex", group.BoxGroup.SenderIndex),
                        new XAttribute("PortIndex", group.BoxGroup.PortIndex),
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
                                new XAttribute("SenderIndex", box.Box.SenderIndex),
                                new XAttribute("PortIndex", box.Box.PortIndex),
                                new XAttribute("ConnectIndex", box.Box.ConnectIndex),
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


        private void LoadConfiguration()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "配置文件 (*.xml)|*.xml";
            dialog.DefaultExt = ".xml";
            dialog.Title = "选择配置文件";

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                BoxGroups = LoadComponentXml(filename);
                _regionManager.Regions[RegionNames.MainRegion].RequestNavigate("/BrightnessAdjustmentView", nr => { });
            }
        }


        private static ObservableCollection<BoxGroupViewModel> LoadComponentXml(string filePath)
        {
            var boxGroupViewModelList = new ObservableCollection<BoxGroupViewModel>();
            XDocument doc = XDocument.Load(filePath);
            if (doc != null)
            {
                IEnumerable<XElement> elementlist = doc.Root.Elements("BoxGroup");

                foreach (var item in elementlist)
                {

                    ObservableCollection<BoxViewModel> boxViewModelList = new ObservableCollection<BoxViewModel>();

                    List<XElement> infoElementList = item.Elements("BoxGroupInfo").ToList();
                    List<XElement> pointElementList = item.Elements("Boxes").ToList();

                    for (int count = 0; count < infoElementList.Count; count++)
                    {

                        LEDBoxGroup boxGroup = new LEDBoxGroup();
                        List<LEDBox> boxList = new List<LEDBox>();
                        foreach (var point in pointElementList[count].Elements("Box"))
                        {
                            XAttribute COMIndexAttr = point.Attribute("COMIndex");
                            XAttribute SenderIndexAttr = point.Attribute("SenderIndex");
                            XAttribute PortIndexAttr = point.Attribute("PortIndex");
                            XAttribute ConnectIndexAttr = point.Attribute("ConnectIndex");
                            XAttribute WidthAttr = point.Attribute("Width");
                            XAttribute HeightAttr = point.Attribute("Height");
                            XAttribute XInPortAttr = point.Attribute("XInPort");
                            XAttribute YInPortAttr = point.Attribute("YInPort");
                            XAttribute XAttr = point.Attribute("X");
                            XAttribute YAttr = point.Attribute("Y");

                            LEDBox box = new LEDBox();
                            box.COMIndex = COMIndexAttr.Value;
                            box.SenderIndex = Convert.ToByte(SenderIndexAttr.Value);
                            box.PortIndex = Convert.ToByte(PortIndexAttr.Value);
                            box.ConnectIndex = Convert.ToByte(ConnectIndexAttr.Value);
                            box.Width = Convert.ToDouble(WidthAttr.Value);
                            box.Height = Convert.ToDouble(HeightAttr.Value);
                            box.XInPort = Convert.ToUInt16(XInPortAttr.Value);
                            box.YInPort = Convert.ToUInt16(YInPortAttr.Value);
                            box.X = Convert.ToUInt16(XAttr.Value);
                            box.Y = Convert.ToUInt16(YAttr.Value);

                            boxList.Add(box);

                            //BoxViewModel boxViewModel = new BoxViewModel(box);
                            //boxViewModelList.Add(boxViewModel);
                        }
                        boxGroup.COMIndex = boxList[0].COMIndex;
                        boxGroup.SenderIndex = boxList[0].SenderIndex;
                        boxGroup.PortIndex = boxList[0].PortIndex;
                        boxGroup.Boxes = boxList;

                        BoxGroupViewModel elementViewModel = new BoxGroupViewModel(boxGroup);

                        XAttribute sysIndexLocationAttr = infoElementList[count].Attribute("IndexLocation");
                        if (sysIndexLocationAttr != null)
                            elementViewModel.IndexLocation = sysIndexLocationAttr.Value;

                        XAttribute sysElementPxPointXAttr = infoElementList[count].Attribute("ElementPxPointX");
                        if (sysElementPxPointXAttr != null)
                            elementViewModel.ElementPxPointX = double.Parse(sysElementPxPointXAttr.Value);

                        XAttribute elementPxPointYAttr = infoElementList[count].Attribute("ElementPxPointY");
                        if (elementPxPointYAttr != null)
                            elementViewModel.ElementPxPointY = double.Parse(elementPxPointYAttr.Value);

                        boxGroupViewModelList.Add(elementViewModel);
                    }


                    #region load
                    //foreach (var infoElement in infoElementList)
                    //{

                    //    LEDBoxGroup groupitem = new LEDBoxGroup();


                    //    XAttribute sysIndexLocationAttr = infoElement.Attribute("IndexLocation");
                    //    if (sysIndexLocationAttr != null)
                    //        elementViewModel.IndexLocation = sysIndexLocationAttr.Value;

                    //    XAttribute sysElementPxPointXAttr = infoElement.Attribute("ElementPxPointX");
                    //    if (sysElementPxPointXAttr != null)
                    //        elementViewModel.ElementPxPointX = double.Parse(sysElementPxPointXAttr.Value);

                    //    XAttribute elementPxPointYAttr = infoElement.Attribute("ElementPxPointY");
                    //    if (elementPxPointYAttr != null)
                    //        elementViewModel.ElementPxPointY = double.Parse(elementPxPointYAttr.Value);

                    //}

                    //IEnumerable<XElement> pointElementList = item.Elements("Boxes");
                    //foreach (var pointElement in pointElementList)
                    //{

                    //    foreach (var point in pointElement.Elements("Box"))
                    //    {
                    //        XAttribute COMIndexAttr = point.Attribute("COMIndex");
                    //        XAttribute SenderIndexAttr = point.Attribute("SenderIndex");
                    //        XAttribute PortIndexAttr = point.Attribute("PortIndex");
                    //        XAttribute ConnectIndexAttr = point.Attribute("ConnectIndex");
                    //        XAttribute WidthAttr = point.Attribute("Width");
                    //        XAttribute HeightAttr = point.Attribute("Height");
                    //        XAttribute XInPortAttr = point.Attribute("XInPort");
                    //        XAttribute YInPortAttr = point.Attribute("YInPort");
                    //        XAttribute XAttr = point.Attribute("X");
                    //        XAttribute YAttr = point.Attribute("Y");

                    //        LEDBox box = new LEDBox();
                    //        box.COMIndex = COMIndexAttr.Value;
                    //        box.SenderIndex = Convert.ToByte(SenderIndexAttr.Value);
                    //        box.PortIndex = Convert.ToByte(PortIndexAttr.Value);
                    //        box.ConnectIndex = Convert.ToByte(ConnectIndexAttr.Value);
                    //        box.Width = Convert.ToDouble(WidthAttr.Value);
                    //        box.Height = Convert.ToDouble(HeightAttr.Value);
                    //        box.XInPort = Convert.ToUInt16(XInPortAttr.Value);
                    //        box.YInPort = Convert.ToUInt16(YInPortAttr.Value);
                    //        box.X = Convert.ToUInt16(XAttr.Value);
                    //        box.Y = Convert.ToUInt16(YAttr.Value);

                    //        BoxViewModel boxViewModel = new BoxViewModel(box);
                    //        boxViewModelList.Add(boxViewModel);
                    //    }
                    //}

                    #endregion

                    //elementViewModel.LEDBoxes = boxViewModelList;
                    //boxGroupViewModelList.Add(elementViewModel);
                }
            }
            return boxGroupViewModelList;
        }


        private async void SetBrightness(object value)
        {
            double[] values = value as double[];
            byte brightnessValue = Convert.ToByte(values[0]);
            byte redValue = Convert.ToByte(values[1]);
            byte greenValue = Convert.ToByte(values[2]);
            byte blueValue = Convert.ToByte(values[3]);
            foreach (var groupViewModel in BoxGroups)
            {
                foreach (var boxViewModel in groupViewModel.LEDBoxes)
                {
                    if (boxViewModel.IsSelected)
                    {
                        await boxViewModel.SetBrightness((byte)(255.0 / 100.0 * brightnessValue));
                        await boxViewModel.SetRGBRed(redValue);
                        await boxViewModel.SetRGBGreen(greenValue);
                        await boxViewModel.SetRGBBlue(blueValue);

                        boxViewModel.ReadDataAsync();
                    }
                }
            }
        }

        private async void SetBrightness(byte value)
        {
            foreach (var groupViewModel in BoxGroups)
            {
                foreach (var boxViewModel in groupViewModel.LEDBoxes)
                {
                    if (boxViewModel.IsSelected)
                    {
                        await boxViewModel.SetBrightness((byte)Math.Ceiling(255.0 / 100.0 * value));
                        boxViewModel.RefreshBrightness();
                    }
                }
            }
        }

        private async void SetRGBRed(byte value)
        {
            foreach (var groupViewModel in BoxGroups)
            {
                foreach (var boxViewModel in groupViewModel.LEDBoxes)
                {
                    if (boxViewModel.IsSelected)
                    {
                        await boxViewModel.SetRGBRed(value);
                        boxViewModel.RefreshRGBRed();
                    }
                }
            }
        }

        private async void SetRGBGreen(byte value)
        {
            foreach (var groupViewModel in BoxGroups)
            {
                foreach (var boxViewModel in groupViewModel.LEDBoxes)
                {
                    if (boxViewModel.IsSelected)
                    {
                        await boxViewModel.SetRGBGreen(value);
                        boxViewModel.RefreshRGBGreen();
                    }
                }
            }
        }

        private async void SetRGBBlue(byte value)
        {
            foreach (var groupViewModel in BoxGroups)
            {
                foreach (var boxViewModel in groupViewModel.LEDBoxes)
                {
                    if (boxViewModel.IsSelected)
                    {
                        await boxViewModel.SetRGBBlue(value);
                        boxViewModel.RefreshRGBBlue();
                    }
                }
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
