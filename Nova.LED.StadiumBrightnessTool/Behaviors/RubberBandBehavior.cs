using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Input;

namespace Nova.LED.StadiumBrightnessTool.Behaviors
{
    [DescriptionAttribute("Enable Rubberband selection for a WPF listbox object.")]
    public class RubberBandBehavior : Behavior<ItemsControl>
    {
        protected override void OnAttached()
        {
            //AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
            AssociatedObject.PreviewMouseMove += AssociatedObject_PreviewMouseMove;
            base.OnAttached();
        }

        void AssociatedObject_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            if (adornerLayer != null)
            {
                RubberBandAdorner adorner = new RubberBandAdorner(AssociatedObject);
                if (adorner != null)
                {
                    adornerLayer.Add(adorner);
                }
            }
            e.Handled = false;
        }

        //void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    RubberBandAdorner band = new RubberBandAdorner(AssociatedObject);
        //    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
        //    adornerLayer.Add(band);
        //}

        protected override void OnDetaching()
        {
            //AssociatedObject.Loaded -= new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
            AssociatedObject.PreviewMouseMove -= AssociatedObject_PreviewMouseMove;
            base.OnDetaching();
        }
    }
}
