using Nova.LED.StadiumBrightnessTool.Controls;
using Nova.LED.StadiumBrightnessTool.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nova.LED.StadiumBrightnessTool
{
    /// <summary>
    /// BrightnessAdjustmentView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class BrightnessAdjustmentView : UserControl
    {
        private ZoomableCanvas _zoomableCanvas;
        public BrightnessAdjustmentView()
        {
            InitializeComponent();
        }

        private void ZoomCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            _zoomableCanvas = sender as ZoomableCanvas;
        }

        private void ItemsControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var x = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            _zoomableCanvas.Scale *= x;

            e.Handled = false;
        }


        private Point? rubberbandSelectionStartPoint = null;

        private void ItemsControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == sender)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(sender as IInputElement));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                // SelectionService.ClearSelection();
                Focus();
                e.Handled = false;
            }
        }

        private void ItemsControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.OriginalSource is System.Windows.Controls.Primitives.Thumb)
            {
                e.Handled = false;
                return;
            }

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(sender as Visual);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(sender as ItemsControl, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = false;
        }


        #region RubberbandAdorner

        //private Point? rubberbandSelectionStartPoint = null;


        //protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    if (e.Source == this)
        //    {
        //        // in case that this click is the start of a 
        //        // drag operation we cache the start point
        //        this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

        //        // if you click directly on the canvas all 
        //        // selected items are 'de-selected'
        //        // SelectionService.ClearSelection();
        //        Focus();
        //        e.Handled = true;
        //    }
        //}

        //protected override void OnPreviewMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    // if mouse button is not pressed we have no drag operation, ...
        //    if (e.LeftButton != MouseButtonState.Pressed)
        //        this.rubberbandSelectionStartPoint = null;

        //    // ... but if mouse button is pressed and start
        //    // point value is set we do have one
        //    if (this.rubberbandSelectionStartPoint.HasValue)
        //    {
        //        // create rubberband adorner
        //        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
        //        if (adornerLayer != null)
        //        {
        //            RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
        //            if (adorner != null)
        //            {
        //                adornerLayer.Add(adorner);
        //            }
        //        }
        //    }
        //    e.Handled = true;
        //}



        #endregion

       
    }
}
