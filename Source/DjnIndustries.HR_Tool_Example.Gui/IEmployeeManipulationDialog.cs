using System;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    /// <summary>
    /// This interface provides the blue print for the dialog window
    /// It can be accessed later on from the data context
    /// </summary>
    public interface IEmployeeManipulationDialog
    {
        /// <summary>
        /// Closes the window
        /// </summary>
        void Close();
    }
}