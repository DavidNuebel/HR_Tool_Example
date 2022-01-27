using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    /// <summary>
    /// Blue print for the UI,
    /// which is accessible later on in the view model
    /// </summary>
    public interface IHJrToolGui
    {
        /// <summary>
        /// Event handler will be fired if the selection of the data grid has changed
        /// </summary>
        event EventHandler SelectionChangedEvent;

        /// <summary>
        /// Event handler will be fired if a selection was double clicked
        /// </summary>
        event EventHandler SelectionDoubleClickEvent;
    }
}
