using DjnIndustries.HR_Tool_Example.Logging;
using DjnIndustries.HR_Tool_Example.Model;
using System;
using System.Windows;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    /// <summary>
    /// Interaction logic for EmployeeManipulationDialog.xaml
    /// </summary>
    public partial class EmployeeManipulationDialog : Window, IEmployeeManipulationDialog
    {
        /// <summary>
        /// Instance of the logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// This event handler will be fired from this window,
        /// if the selection in the data grid has changed
        /// </summary>
        public event EventHandler SelectionChangedEvent;

        /// <summary>
        /// This constructor is used for adding an employee
        /// </summary>
        public EmployeeManipulationDialog(ILogger logger)
        {
            InitializeComponent();

            // Check if logger reference is null and throw exception
            if(logger == null)
                throw new ArgumentNullException(nameof(logger));

            // Save logger reference 
            _logger = logger;

            // Set view model as data context
            DataContext = new EmployeeManipulationDialogViewModel(logger, this);
        }

        /// <summary>
        /// This constructor is used for editing an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeManipulationDialog(ILogger logger, Employee employee) : this(logger) // This "this" indicates, that the first constructor should be executed
        {
            // Check if employee is null and throw exception
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            // Set data context
            DataContext = new EmployeeManipulationDialogViewModel(logger, this, employee);
        }

        /// <summary>
        /// Shows window as a dialog and returns and employee
        /// </summary>
        /// <returns></returns>
        public virtual new Employee? ShowDialog()
        {
            // Run basic method for showing the dialog
            base.ShowDialog();

            //get the view model from the data context
            var viewModel = DataContext as EmployeeManipulationDialogViewModel;

            // Check values of view model and employee
            if (viewModel == null ||
                viewModel.Employee == null ||
                string.IsNullOrEmpty(viewModel.Employee.FirstName) ||
                string.IsNullOrEmpty(viewModel.Employee.LastName) || 
                viewModel.Employee.Salary < 0)
            {
                // Log event
                _logger.Log("Empty values were tried to be saved. Canceled the action.");
                return null;
            }

            // Return validated employee data
            return viewModel.Employee;
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        public virtual new void Close()
        {
            // Basic method for closing the window
            base.Close();
        }
    }
}