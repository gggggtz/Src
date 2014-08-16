using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Primitives;
using Common.Extensions;
using System.Windows.Data;
using System.Collections;

namespace Common.Wpf.Extension
{
    public static class FrameworkElementExtension
    {
        public static List<DependencyProperty> GetDependencyProperties(this FrameworkElement owner)
        {
            List<DependencyProperty> properties = new List<DependencyProperty>();
            MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(owner);
            if (markupObject != null)
            {
                markupObject.Properties.Foreach(p =>
                    {
                        if (p.DependencyProperty != null)
                        {
                            properties.Add(p.DependencyProperty);
                        }
                    });
            }
            return properties;
        }

        public static List<DependencyProperty> GetAttachedDependencyProperties(this FrameworkElement owner)
        {
            List<DependencyProperty> properties = new List<DependencyProperty>();
            MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(owner);
            if (markupObject != null)
            {
                markupObject.Properties.Foreach(p =>
                {
                    if (p.DependencyProperty != null && p.IsAttached)
                    {
                        properties.Add(p.DependencyProperty);
                    }
                });
            }
            return properties;
        }

        public static List<Binding> GetBindings(this FrameworkElement owner, bool recursive = false)
        {
            List<Binding> bindings = new List<Binding>();
            GetDependencyProperties(owner).ForEach(dp =>
                {
                    Binding b = BindingOperations.GetBinding(owner, dp);
                    if (b != null)
                    {
                        bindings.Add(b);
                    }
                });


            if (recursive)
            {
                LogicalTreeHelper.GetChildren(owner).Foreach(o =>
                    {
                        FrameworkElement child = o as FrameworkElement;
                        if (child != null)
                        {
                            bindings.AddRange(child.GetBindings(recursive));
                        }
                    });
            }
            return bindings;
        }
    }
}
