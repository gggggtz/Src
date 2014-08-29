using Common.Wpf.ViewModel;
using Controls.CustomWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MsTools
{
    public abstract partial class WindowBase<ActionType> : ParentWindow<ActionType> where ActionType : struct
    {
        protected DockPanel topPanel;
        protected Border windowButtons;
        protected Grid windowGrid;
        protected Border windowBorder;
        protected Thumb dragThumb;
        protected WindowViewModel dataContext;

        public WindowBase(WindowViewModel dc)
        {
            dataContext = dc;
            Template = FindResource("WindowTemplate") as ControlTemplate;
            ApplyTemplate();
            Initialize();
            DataContext = dc;
            //MinHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            MinHeight = SystemParameters.FullPrimaryScreenHeight;
        }

        private void Initialize()
        {
            windowBorder = Template.FindName("windowBorder", this) as Border;
            windowGrid = Template.FindName("windowGrid", this) as Grid;
            topPanel = Template.FindName("topPanel", this) as DockPanel;
            windowButtons = Template.FindName("windowButtons", this) as Border;
            dragThumb = Template.FindName("dragThumb", this) as Thumb;
            dragThumb.DragDelta += dragThumb_DragDelta;
            //topPanel.MouseMove += DockPanel_MouseMove;
        }

        void dragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Top += e.VerticalChange;
            Left += e.HorizontalChange;
            RaisePositionChanged(e.HorizontalChange, e.VerticalChange);
        }

        protected void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                this.DragMove();
        }

        protected override Decorator GetWindowButtonsPlaceholder()
        {
            return windowButtons;
        }
    }
}
