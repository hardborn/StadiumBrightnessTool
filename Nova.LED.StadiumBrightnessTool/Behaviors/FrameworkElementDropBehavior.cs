
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private Type dataType;
        private FrameworkElementAdorner adorner;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            this.AssociatedObject.DragOver -= AssociatedObject_DragOver;
            this.AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (this.dataType == null)
            {
                if (this.AssociatedObject.DataContext != null)
                {
                    IDropable dropObject = this.AssociatedObject.DataContext as IDropable;
                    if (dropObject != null)
                    {
                        this.dataType = dropObject.DataType;
                    }
                }
            }
            if (this.adorner == null)
                this.adorner = new FrameworkElementAdorner(sender as UIElement);
               // this.adorner = new Adorner(sender as UIElement);
            e.Handled = false;
        }

        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (this.dataType != null)
            {
                if (e.Data.GetDataPresent(dataType))
                {
                    e.Effects = DragDropEffects.Copy;

                    if (this.adorner != null)
                        this.adorner.Update();
                }
            }
            e.Handled = false;
        }

        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (this.adorner != null)
                this.adorner.Remove();
            e.Handled = false;
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (this.dataType != null)
            {
                if (e.Data.GetDataPresent(this.dataType))
                {
                    ItemsControl sandboxView = this.AssociatedObject as ItemsControl;
                    if (sandboxView != null)
                    {
                        ItemsPresenter itemsPresenter = GetFirstVisualChild<ItemsPresenter>(sandboxView);
                        ZoomableCanvas mapCanvas = GetFirstVisualChild<ZoomableCanvas>(itemsPresenter);
                        IDropable target = this.AssociatedObject.DataContext as IDropable;
                        Point position = e.GetPosition(mapCanvas);
                        target.Drop(e.Data.GetData(dataType), position);
                    }                    
                }
            }
            if (this.adorner != null)
                this.adorner.Remove();

            e.Handled = false ;
        }

        private static T GetFirstVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetFirstVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
