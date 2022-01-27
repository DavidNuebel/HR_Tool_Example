#pragma warning disable 67

using System;
using System.Windows.Input;

namespace DjnIndustries.HR_Tool_Example.Gui.Util
{
    // Hint: This class is a helper class and can be done better, worse or different :D

    /// <summary>
    /// Can handle a button in a few lines of code
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Action to execute
        /// </summary>
        private readonly Action Action;

        /// <summary>
        /// <para>Action to check if first action can be executed</para>
        /// <para>Can be disabled within the constructor (<see cref="DelegateCommand"/>)</para>
        /// </summary>
        private readonly Func<object, bool> CanExecuteAction;

        /// <summary>
        /// Flag for automatically checking before executing
        /// </summary>
        private readonly bool CheckBeforeExecution;

        /// <summary>
        /// Can handle a button in a few lines of code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="canExecute">Action for checking if the first action can be executed or not</param>
        /// <param name="checkBeforeExecution">Flag for automatically checking before executing</param>
        public DelegateCommand(Action action, Func<object, bool> canExecute = null, bool checkBeforeExecution = false)
        {
            if (action != null)
            {
                Action = action;
            }
            else
            {
                Action = () => { };
            }

            if (canExecute != null)
            {
                CanExecuteAction = canExecute;
            }
            else
            {
                CanExecuteAction = new Func<object, bool>((obj) => true);
            }
            CheckBeforeExecution = checkBeforeExecution;
        }

        /// <summary>
        /// <b>OBSOLETE!</b> Would be risen if the state of execution has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Checks, if the first configured action can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter = null) => CanExecuteAction(parameter);

        /// <summary>
        /// <para>Executes the configured action.</para>
        /// <para>If <see cref="CheckBeforeExecution"/> is configured to <b>true</b> in the constructor (<see cref="DelegateCommand"/>)</para>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter = null)
        {
            if (CheckBeforeExecution && !CanExecute(parameter))
                return;

            Action();
        }

        /// <summary>
        /// <b>OBSOLETE!</b> Manually raise <see cref="CanExecuteChanged"/> if state of execution changes.
        /// </summary>
        private void RaiseCanExecuteChanged()
        { }
    }

    /// <summary>
    /// Can handle a button with a parameter in a few lines of code
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// Action to execute
        /// </summary>
        private readonly Action<T> Action;

        /// <summary>
        /// <para>Action to check if first action can be executed</para>
        /// <para>Can be disabled within the constructor (<see cref="DelegateCommand"/>)</para>
        /// </summary>
        private readonly Func<object, bool> CanExecuteAction;

        /// <summary>
        /// Flag for automatically checking before executing
        /// </summary>
        private readonly bool CheckBeforeExecution;

        /// <summary>
        /// Can handle a button a parameter in a few lines of code
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="canExecute">Action for checking if the first action can be executed or not</param>
        /// <param name="checkBeforeExecution">Flag for automatically checking before executing</param>
        public DelegateCommand(Action<T> action, Func<object, bool> canExecute = null, bool checkBeforeExecution = false)
        {
            if (action != null)
            {
                Action = action;
            }
            else
            {
                Action = (obj) => { };
            }

            if (canExecute != null)
            {
                CanExecuteAction = canExecute;
            }
            else
            {
                CanExecuteAction = new Func<object, bool>((obj) => true);
            }
            CheckBeforeExecution = checkBeforeExecution;
        }

        /// <summary>
        /// <b>OBSOLETE!</b> Would be risen if the state of execution has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Checks, if the first configured action can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter = null) => CanExecuteAction(parameter);

        /// <summary>
        /// <para>Executes the configured action.</para>
        /// <para>If <see cref="CheckBeforeExecution"/> is configured to <b>true</b> in the constructor (<see cref="DelegateCommand"/>)</para>
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter = null)
        {
            if (CheckBeforeExecution && !CanExecute(parameter))
                return;

            Action((T)parameter);
        }

        /// <summary>
        /// <b>OBSOLETE!</b> Manually raise <see cref="CanExecuteChanged"/> if state of execution changes.
        /// </summary>
        private void RaiseCanExecuteChanged()
        { }
    }
}