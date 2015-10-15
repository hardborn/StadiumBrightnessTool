using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool _isMouseClicked = false;
        private Point _startPoint;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
            this.AssociatedObject.MouseLeave -= AssociatedObject_MouseMove;
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = true;
            _startPoint = e.GetPosition(App.Current.MainWindow);
            e.Handled = false;
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = false;
            e.Handled = false;
        }

        void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseClicked)
            {
                Point mousePos = e.GetPosition(App.Current.MainWindow);
                Vector diff = _startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed  )
                    //&&
                    //(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    //Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    IDragable dragObject = this.AssociatedObject.DataContext as IDragable;
                    if (dragObject != null)
                    {
                        DataObject data = new DataObject();
                        data.SetData(dragObject.DataType, dragObject.Data);
                        DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Copy);
                    }
                }
               
            }
            
            e.Handled = false;
        }
    }
}
