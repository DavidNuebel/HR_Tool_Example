using DjnIndustries.HR_Tool_Example.DataPersistence;
using DjnIndustries.HR_Tool_Example.Gui.Util;
using DjnIndustries.HR_Tool_Example.Logging;
using DjnIndustries.HR_Tool_Example.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DjnIndustries.HR_Tool_Example.Gui
{
    /// <summary>
    /// <para>This class is the view model</para>
    /// </summary>
    public class HrToolGuiViewModel : NotifyPropertyChanged // This class inherits the NotfifyPropertyChanged class in the Utility folder
    {
        #region Interfaces
        /// <summary>
        /// Logger reference
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Gui reference
        /// </summary>
        private readonly IHJrToolGui _gui;

        /// <summary>
        /// Interface referring to the persistence agent which
        /// was passed in by the constructor dependency injection
        /// </summary>
        private readonly IPersistenceAgent _persistenceAgent;
        #endregion

        #region Displayed Values
        /// <summary>
        /// Holds the title of the window
        /// </summary>
        public readonly string Title = "HR Tool Example";

        /// <summary>
        /// Holds the value of the average salary
        /// </summary>
        public int AverageSalary { get; set; }

        /// <summary>
        /// Count of how many employees are saved
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// State of edit button
        /// It will be set to true if
        /// a item is selected in the data grid
        /// </summary>
        public bool EditEnabled { get; set; }

        /// <summary>
        /// State of remove button
        /// It will be set to true if
        /// a item is selected in the data grid
        /// </summary>
        public bool RemoveEnabled { get; set; }

        /// <summary>
        /// This list contains all employees. It is bound to the UI.
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }

        /// <summary>
        /// This property contains the currently selected employee
        /// It is null when nothing is selected and this property
        /// is bound to the UI.
        /// </summary>
        public Employee SelectedEmployee { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// This command is bound to a button
        /// </summary>
        public DelegateCommand AddCommand { get; set; }

        /// <summary>
        /// This command is also bound to a button
        /// </summary>
        public DelegateCommand EditCommand { get; set; }

        /// <summary>
        /// And this command is bound to a button to
        /// </summary>
        public DelegateCommand RemoveCommand { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// This constructor builds up the view model with all its properties
        /// and methods needed for the UI to have a data source
        /// </summary>
        public HrToolGuiViewModel(ILogger logger, IHJrToolGui gui, IPersistenceAgent persistenceAgent)
        {
            // Check if logger is null, if yes throw and ArgumentNullException
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            // Save logger in this class to access it later on
            _logger = logger;

            // Check if gui reference is null, if yes throw and ArgumentNullException
            if (gui == null)
                throw new ArgumentNullException(nameof(logger));

            // Save gui reference in this class to access it later on
            _gui = gui;

            // Check if persistence agent is null, if yes throw and ArgumentNullException
            if (persistenceAgent == null)
                throw new ArgumentNullException(nameof(persistenceAgent));

            // Save persistence agent in this class to access it later on
            _persistenceAgent = persistenceAgent;

            // Bind selection changed to a method in this class
            _gui.SelectionChangedEvent += SelectionChanged;

            // Bind double click on data grid to a method in this class
            _gui.SelectionDoubleClickEvent += SelectionDoubleClick;

            // Initialize the employees list
            Employees = new ObservableCollection<Employee>();

            // Set up commands for the buttons
            AddCommand = new DelegateCommand(AddEmployee);
            EditCommand = new DelegateCommand(EditEmployee);
            RemoveCommand = new DelegateCommand(RemoveEmployee);

            // Update UI
            UpdateUI();
        }
        #endregion

        #region Command Methods
        /// <summary>
        /// Opens up the add dialog and waits for completion
        /// </summary>
        public void AddEmployee()
        {
            // Create new add dialog
            var dialogWindow = new EmployeeManipulationDialog(_logger);

            // Show window as a dialog and wait for the result,
            // which is an employee
            var employee = dialogWindow.ShowDialog();

            // Check if newly created employee is null
            if (employee == null)
            {
                // Log event
                _logger.Log("It was tried to create a new employee, but it was completely empty.");
                return;
            }

            Employees.Add(employee); // Add the new employee to the data grids item source
            _persistenceAgent.Update(Employees.ToArray()); // Update data in storage
            UpdateUI(); // Update UI

            // Log event
            _logger.Log("A new employee was created.");
        }

        /// <summary>
        /// Opens up the edit dialog and waits for completion
        /// </summary>
        public void EditEmployee()
        {
            // If no employee is selected in the data grid, exit method
            if (SelectedEmployee == null)
            {
                // Log event
                _logger.Log($"It was tried to edit a employee, but no data was selected.");
                return;
            }

            // Create dialog instance
            var dialogWindow = new EmployeeManipulationDialog(_logger, SelectedEmployee);

            // Show window as dialog and wait for the dialog to be 
            // closed, as the return value will be the edited person 
            var employee = dialogWindow.ShowDialog();

            // If no employee will be returned, exit method
            if (employee == null)
            {
                // Log event
                _logger.Log($"Edited employee");
                return;
            }

            // Get index of the selected employee
            int index = Employees.ToList().FindIndex(e => e == SelectedEmployee);

            // If the index is not -1 (error), update employee in data
            if (index != -1)
            {
                Employees[index] = employee; // Set edited employee to the old ones index
                _persistenceAgent.Update(Employees); // Update data in storage
                UpdateUI(); // Update UI

                // Log event
                _logger.Log($"Edited employee");
            }
            else
            {
                // Log event
                _logger.Log($"Employee could not be edited");
            }
        }

        /// <summary>
        /// Removes the selected employee
        /// </summary>
        public void RemoveEmployee()
        {
            // Check if selected employee is not null
            if (SelectedEmployee != null)
            {
                // Remove the employee from the list and update the list
                Employees.Remove(SelectedEmployee);
                _persistenceAgent.Update(Employees); // Update data in storage
                UpdateUI(); // Update UI
            }
        }
        #endregion

        #region Data
        /// <summary>
        /// Loads employees from a file and stores it in the Employees list
        /// </summary>
        public void LoadEmployees()
        {
            // Load employees from data storage
            var listOfEmployees = _persistenceAgent.Load<Employee[]>();

            // Check if data is null
            if (listOfEmployees == null)
            {
                // Create empty list
                Employees = new ObservableCollection<Employee>();
            }
            else
            {
                // Create list with data
                Employees = new ObservableCollection<Employee>(listOfEmployees);
            }
            UpdateUI(); // Update UI
        }
        #endregion

        #region UI
        /// <summary>
        /// Refreshes or sets new values to the properties,
        /// which are bound to the UI
        /// </summary>
        public void UpdateUI()
        {
            // Update buttons states
            EditEnabled = SelectedEmployee != null;
            RemoveEnabled = SelectedEmployee != null;

            // Update statistics
            EmployeeCount = Employees.Count();

            if (Employees.Count() < 1)
            {
                AverageSalary = 0;
            }
            else
            {
                AverageSalary = (int)Employees.Average(e => e.Salary);
            }

            // Raise update events on properties
            RaisePropertyChanged(nameof(EditEnabled));
            RaisePropertyChanged(nameof(RemoveEnabled));
            RaisePropertyChanged(nameof(Employees));
            RaisePropertyChanged(nameof(AverageSalary));
            RaisePropertyChanged(nameof(EmployeeCount));
        }

        /// <summary>
        /// This method is called from the event in the gui reference,
        /// which was triggered from the window
        /// </summary>
        /// <param name="sender">Object of the sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectionChanged(object? sender, EventArgs e)
        {
            // Update UI
            UpdateUI();
        }

        /// <summary>
        /// This method is called from the event in the gui reference,
        /// which was triggered from the window
        /// </summary>
        /// <param name="sender">Object of the sender</param>
        /// <param name="e">Event arguments</param>
        private void SelectionDoubleClick(object? sender, EventArgs e)
        {
            // Edit employee
            EditEmployee();
        }
        #endregion
    }
}