using Nova.LED.StadiumBrightnessTool.ViewModel;
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
        private Brush brush;

        private ItemsControl itemsControl;

        public RubberbandAdorner(ItemsControl designerCanvas, Point? dragStartPoint)
            : base(designerCanvas)
        {
            this.itemsControl = designerCanvas;
            this.startPoint = dragStartPoint;
            rubberbandPen = new Pen(Brushes.LightSlateGray, 1);
            rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
            brush = new SolidColorBrush(SystemColors.HighlightColor);
            brush.Opacity = 0.3;
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

            DisposeRubberBand();
            // remove this adorner from adorner layer
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AdornedElement);
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
                dc.DrawRectangle(brush, rubberbandPen, new Rect(this.startPoint.Value, this.endPoint.Value));
        }

        private void UpdateSelection()
        {

            Rect rubberBand = new Rect(startPoint.Value, endPoint.Value);
            ItemsControl _selector = AdornedElement as ItemsControl;
            foreach (var obj in _selector.Items)
            {

                ContentPresenter item = _selector.ItemContainerGenerator.ContainerFromItem(obj) as ContentPresenter;
                if (item == null)
                {
                    break;
                }
                var boxesControl = FindVisualChild<ItemsControl>(item);

                if (boxesControl != null)
                {
                    foreach (var boxControl in boxesControl.Items)
                    {
                        ContentPresenter presenter = boxesControl.ItemContainerGenerator.ContainerFromItem(boxControl) as ContentPresenter;
                        var viewModel = presenter.DataContext as BoxViewModel;
                        Point point = presenter.TransformToAncestor(AdornedElement).Transform(new Point(0, 0));
                        Rect bandrect = new Rect(startPoint.Value, endPoint.Value);
                        if (bandrect.Height < 0.1 && bandrect.Width <0.1 )
                        {
                            break;
                        }
                        Rect elementrect = new Rect(point.X, point.Y, presenter.ActualWidth, presenter.ActualHeight);

                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            if (bandrect.IntersectsWith(elementrect))
                            {
                                viewModel.IsSelected = true;
                            }
                        }
                        else
                        {
                            if (bandrect.IntersectsWith(elementrect))
                            {
                                viewModel.IsSelected = true;
                            }
                            else
                            {
                                viewModel.IsSelected = false;
                            }
                        }                       
                    }                    
                }              
            }          
        }

        private void DisposeRubberBand()
        {
            startPoint = null;
            endPoint = null;
            //flag = false;
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
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
