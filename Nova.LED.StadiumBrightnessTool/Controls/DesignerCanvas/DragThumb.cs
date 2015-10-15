using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Nova.LED.StadiumBrightnessTool.Controls
{
    public class DragThumb : Thumb
    {
        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
        }

        static DragThumb()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DragThumb), new FrameworkPropertyMetadata(typeof(DragThumb)));


        }

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ItemsControl item = this.DataContext as ItemsControl;

            ContentPresenter contentPresenter = VisualTreeHelper.GetParent(item) as ContentPresenter;
            
            double minLeft = double.MaxValue;
            double minTop = double.MaxValue;
            double left = Canvas.GetLeft(contentPresenter);
            double top = Canvas.GetTop(contentPresenter);

            minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
            minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

            double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
            double deltaVertical = Math.Max(-minTop, e.VerticalChange);

            if (double.IsNaN(left)) left = 0;
            if (double.IsNaN(top)) top = 0;

            Canvas.SetLeft(contentPresenter, left + deltaHorizontal);
            Canvas.SetTop(contentPresenter, top + deltaVertical);

            e.Handled = false;
        }
        //}
    }
}
