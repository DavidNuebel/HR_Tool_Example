using System;
using System.Windows;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    /// <summary>
    /// <para>This class is used by default for initializing the UI components in the .xaml file</para>
    /// <para>
    ///     The view model will not set here. Furthermore it will be passed from
    ///     the <typeparamref name="App"/> class in <typeparamref name="DjnIndustries.HR_Tool_Example.App"/></para>
    /// </summary>
    public partial class HrToolGui : Window, IHJrToolGui // HrToolGui inherits the class Window and got its properties and methods
    {
        /// <summary>
        /// Event handler will be fired from this window,
        /// if the selection of the data grid has changed
        /// </summary>
        public event EventHandler SelectionChangedEvent;

        /// <summary>
        /// Event handler will be fired if a selection was double clicked
        /// </summary>
        public event EventHandler SelectionDoubleClickEvent;

        /// <summary>
        /// <para>In this constructor the components in the UI will be initialized.</para>
        /// </summary>
        public HrToolGui()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fires the selection changed event
        /// </summary>
        /// <param name="sender">Object of the sender</param>
        /// <param name="e">Event arguments</param>
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Invoke the event
            SelectionChangedEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Fires the selection changed event
        /// </summary>
        /// <param name="sender">Object of the sender</param>
        /// <param name="e">Event arguments</param>
        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Invoke the event
            SelectionDoubleClickEvent?.Invoke(this, e);
        }
    }
}