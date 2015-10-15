using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Nova.LED.StadiumBrightnessTool.ViewModel;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    public class RubberBandAdorner : Adorner
    {
        private Point startpoint;
        private Point currentpoint;
        private Brush brush;
        private bool flag;
        //private ScrollViewer viewer;
        //private ScrollBar scrollbar;

        public RubberBandAdorner(UIElement adornedElement)
            :base(adornedElement)
        {
            IsHitTestVisible = false;
            adornedElement.PreviewMouseMove += new MouseEventHandler(adornedElement_PreviewMouseMove);
            adornedElement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(adornedElement_PreviewMouseLeftButtonDown);
            adornedElement.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(adornedElement_PreviewMouseLeftButtonUp);
            brush = new SolidColorBrush(SystemColors.HighlightColor);
            brush.Opacity = 0.3;
        }

        void adornedElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DisposeRubberBand();
        }

        void adornedElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ListBox _selector = AdornedElement as ListBox;
            //if (_selector.SelectedItems != null && (_selector.SelectionMode == SelectionMode.Extended || _selector.SelectionMode == SelectionMode.Multiple))
            //{
            //    _selector.SelectedItems.Clear();
            //}
            startpoint = Mouse.GetPosition(this.AdornedElement);
            Mouse.Capture(AdornedElement);
            flag = true;
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

        void adornedElement_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && flag)
            {
                Rect rubberBand = new Rect(startpoint, currentpoint);
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
                            Rect bandrect = new Rect(startpoint, currentpoint);
                            if (bandrect.Height < 0.1 && bandrect.Width < 0.1)
                            {
                                break;
                            }
                            Rect elementrect = new Rect(point.X, point.Y, presenter.ActualWidth, presenter.ActualHeight);
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(startpoint, currentpoint);
            drawingContext.DrawGeometry(brush, new Pen(SystemColors.HighlightBrush, 1), new RectangleGeometry(rect));
            base.OnRender(drawingContext);
        }

        private void DisposeRubberBand()
        {
            currentpoint = new Point(0, 0);
            startpoint = new Point(0, 0);
            AdornedElement.ReleaseMouseCapture();
            InvalidateVisual();
            flag = false;
        }
    }
}
