using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Nova.LED.StadiumBrightnessTool.Controls
{
    public class RubberbandAdorner : Adorner
    {
        private Point? startPoint;
        private Point? endPoint;
        private Pen rubberbandPen;

        private ZoomableCanvas zoomableCanvas;

        public RubberbandAdorner(ZoomableCanvas designerCanvas, Point? dragStartPoint)
            : base(designerCanvas)
        {
            this.zoomableCanvas = designerCanvas;
            this.startPoint = dragStartPoint;
            rubberbandPen = new Pen(Brushes.LightSlateGray, 1);
            rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();

                endPoint = e.GetPosition(this);
                UpdateSelection();
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }

            e.Handled = true;
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            // release mouse capture
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            // remove this adorner from adorner layer
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.zoomableCanvas);
            if (adornerLayer != null)
                adornerLayer.Remove(this);

            e.Handled = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // without a background the OnMouseMove event would not be fired!
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));

            if (this.startPoint.HasValue && this.endPoint.HasValue)
                dc.DrawRectangle(Brushes.Transparent, rubberbandPen, new Rect(this.startPoint.Value, this.endPoint.Value));
        }

        private void UpdateSelection()
        {
            //zoomableCanvas.SelectionService.ClearSelection();

            Rect rubberBand = new Rect(startPoint.Value, endPoint.Value);

            ItemsControl _selector = AdornedElement as ItemsControl;
            //foreach (var obj in _selector.Items)
            //{

            //    ContentPresenter item = _selector.ItemContainerGenerator.ContainerFromItem(obj) as ContentPresenter;

            //    var itemcontrol = FindVisualChild<ItemsControl>(item);

            //    if (itemcontrol == null)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}
            foreach (ContentPresenter item in zoomableCanvas.Children)
            {

                ContentPresenter content = _selector.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;

                var itemcontrol = FindVisualChild<ItemsControl>(content);

                if (itemcontrol == null)
                {

                }
                else
                {

                }
                //Rect itemRect = VisualTreeHelper.GetDescendantBounds(item);
                //Rect itemBounds = item.TransformToAncestor(zoomableCanvas).TransformBounds(itemRect);

                //if (rubberBand.Contains(itemBounds))
                //{
                //    //if (item is Connection)
                //    //   // zoomableCanvas.SelectionService.AddToSelection(item as ISelectable);
                //    //else
                //    //{
                //    //    DesignerItem di = item as DesignerItem;
                //    //    if (di.ParentID == Guid.Empty)
                //    //       // zoomableCanvas.SelectionService.AddToSelection(di);
                //    //}
                //}
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
  where childItem : DependencyObject
        {
            // Search immediate children first (breadth-first)
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)
                    return (childItem)child;

                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);

                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }
    }
}
