using DjnIndustries.HR_Tool_Example.Gui.Util;
using DjnIndustries.HR_Tool_Example.Logging;
using DjnIndustries.HR_Tool_Example.Model;
using System;
using System.Windows;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    public class EmployeeManipulationDialogViewModel
    {
        #region Interfaces
        /// <summary>
        /// Reference to the logger instance
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Reference to the window
        /// </summary>
        private readonly IEmployeeManipulationDialog _window;
        #endregion

        #region Commands
        /// <summary>
        /// This command is bound to a the save button
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// This command is also bound to the cancel button
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }
        #endregion

        /// <summary>
        /// Employee instance where all things will be saved to
        /// </summary>
        public Employee Employee { get; set; }

        #region Displayed Values
        /// <summary>
        /// First name of the employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employees date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Employees date of hire
        /// </summary>
        public DateTime DateOfHire { get; set; }

        /// <summary>
        /// Employees salary
        /// </summary>
        public int Salary { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// This view model wants a interface which is implemented 
        /// in the code behind class to close the window
        /// </summary>
        /// <param name="window">Window reference</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeManipulationDialogViewModel(ILogger logger, IEmployeeManipulationDialog window)
        {
            // Check if reference to logger is null
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            // Save logger reference
            _logger = logger;

            // Check if reference to window is null
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            // Store window reference
            _window = window;

            // Set up commands for the buttons
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Close);

            // Set properties to a place holder value
            FirstName = "";
            LastName = "";
            DateOfBirth = DateTime.Today;
            DateOfHire = DateTime.Today;
            Salary = 0;

            // Save current UI values to object
            Employee = new Employee(FirstName, LastName, DateOfBirth, DateOfHire, Salary);
        }

        /// <summary>
        /// This constructor wants a interface which is implemented
        /// in the code behind class and an employee object
        /// 
        /// It will load all available information into the UI
        /// </summary>
        /// <param name="window"></param>
        /// <param name="employee"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeManipulationDialogViewModel(ILogger logger, IEmployeeManipulationDialog window, Employee employee) : this(logger, window)
        {
            // Check if employee is null and throw exception
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            // Load values from employee into properties
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            DateOfBirth = employee.DateOfBirth;
            DateOfHire = employee.DateOfHire;
            Salary = employee.Salary;

            // Save current UI values to object
            Employee = new Employee(FirstName, LastName, DateOfBirth, DateOfHire, Salary);
        }
        #endregion

        #region Commands Methods
        /// <summary>
        /// Saves the available data into the employee instance and closes the window
        /// </summary>
        public void Save()
        {
            // Check for empty first name
            if(string.IsNullOrEmpty(FirstName))
            {
                // Log event
                _logger.Log("First name was left empty");
                MessageBox.Show("The first name was left empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check for empty last name
            if (string.IsNullOrEmpty(LastName))
            {
                // Log event
                _logger.Log("Last name was left empty.");
                MessageBox.Show("The last name was left empty!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if employee is under 16
            var age = DateTime.Now.Subtract(DateOfBirth);
            if (age < TimeSpan.FromDays(365*16))
            {
                // Log event
                _logger.Log("Employee must at least have an age of 16.");
                MessageBox.Show("Employee must at least have an age of 16!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check for negative salary
            if (Salary < 0)
            {
                // Log event
                _logger.Log("The salary cant be negative");
                MessageBox.Show("The salary cant be negative", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // If everything is fine, create employee object to be read out and close the window
            Employee = new Employee(FirstName, LastName, DateOfBirth, DateOfHire, Salary);
            _logger.Log("Employee must at least have an age of 16."); // Log event
            Close(); // Close the window
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        public void Close()
        {
            _window.Close();
        }
        #endregion
    }
}