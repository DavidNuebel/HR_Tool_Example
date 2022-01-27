using DjnIndustries.HR_Tool_Example.DataPersistence;
using DjnIndustries.HR_Tool_Example.Gui;
using DjnIndustries.HR_Tool_Example.Logging;
using DjnIndustries.HR_Tools_Example.IO;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;

/// <summary>
/// <para>This name space is put together like this like convention: CompanyName.ToolorLibraryName.PackageName</para>
/// </summary>
namespace DjnIndustries.HR_Tool_Example.App
{
    /// <summary>
    /// <para>This class contains the entry point of the entire application</para>
    /// <para>Requirements for dependency injection and MVVM pattern will be setup in here.</para>
    /// <para>
    ///     Dependency Injection:
    ///     <see href="https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection"/>
    /// </para>
    /// <para>
    ///     MVVM:
    ///     <see href="https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern"/>
    /// </para>
    /// <para>
    ///     Comments:
    ///     <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments"/>
    /// </para>
    /// </summary>

    // Excludes this code from the overall code coverage: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
    // This is not needed, as this version contains no unit tests
    // [ExcludeFromCodeCoverage] 
    public partial class App : Application // App inherits Application, which got a ton of properties and methods: https://docs.microsoft.com/en-us/dotnet/api/system.windows.application?view=windowsdesktop-6.0
    {
        /*
         * Variables that contain and paths
         */
        private static string sessionId = $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}"; // Just a random id which is used for this session
        private readonly string DATA_STORAGE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"HR_Tool_Example_{sessionId}.data");
        private readonly string LOG_STORAGE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"HR_Tool_Example_{sessionId}.log");

        /// <summary>
        /// <para>View model of the UI</para>
        /// </summary>
        private HrToolGuiViewModel _viewModel;

        /// <summary>
        /// <para>Gui window</para>
        /// </summary>
        private HrToolGui _gui;

        /// <summary>
        /// File handler instance that will be passed to the logger instance
        /// </summary>
        private FileHandler _logFileHandler;

        /// <summary>
        /// File handler instance that will be passed to the persistence agent instance
        /// </summary>
        private FileHandler _persistenceAgentFileHandler;

        /// <summary>
        /// Logger instance passed into view model and components
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// Persistence agent instance passed into view model
        /// </summary>
        private PersistenceAgent _persistenceAgent;

        /// <summary>
        /// <para>
        ///     The constructor will be called at before <see cref="OnStartup(StartupEventArgs)"/>.
        ///     Don't execute heavy tasks here, that would be bad practice.
        /// </para>
        /// </summary>
        public App()
        {
            // Can be left empty, as nothing will be done here
        }

        /// <summary>
        /// <para>
        ///     Entry point of the program.
        /// </para>
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Setting up the file handler instances
            _logFileHandler = new FileHandler(LOG_STORAGE_PATH);
            _persistenceAgentFileHandler = new FileHandler(DATA_STORAGE_PATH);

            // Setting up the logger instance
            _logger = new Logger(_logFileHandler);

            // Setting up the persistence agent instance
            _persistenceAgent = new PersistenceAgent(_logger, _persistenceAgentFileHandler);

            // Setting up window and view model objects
            _gui = new HrToolGui();

            /*
             * If necessary, pass in a interface which is implemented by the GUI.
             * This will *break* the idea of MVVM, but therefore some more options
             * regarding UI control can be achieved. This should only be done if no
             * other option is available.
             */
            _viewModel = new HrToolGuiViewModel(_logger, _gui, _persistenceAgent);

            // Load available employees
            _viewModel.LoadEmployees();

            // Set view model as data context
            _gui.DataContext = _viewModel;

            // Set UI as main window
            MainWindow = _gui;

            // Show UI
            MainWindow.Show();
        }
    }
}