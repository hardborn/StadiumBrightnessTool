
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    public class FramworkElementDropBehavior : Behavior<FrameworkElement>
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
                    //MapSandboxControl sandboxView = this.AssociatedObject as MapSandboxControl;
                    //if (sandboxView != null)
                    //{
                    //    ControlTemplate template = sandboxView.Template;
                    //    ZoomableCanvas mapCanvas = template.FindName("MapCanvas", sandboxView) as ZoomableCanvas;
                    //    IDropable target = this.AssociatedObject.DataContext as IDropable;
                    //    Point position = e.GetPosition(mapCanvas);
                    //    target.Drop(e.Data.GetData(dataType), position);
                    //}
                    
                }
            }
            if (this.adorner != null)
                this.adorner.Remove();

            e.Handled = false ;
        }
    }
}
