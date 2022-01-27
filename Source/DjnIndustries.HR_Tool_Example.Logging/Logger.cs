using DjnIndustries.HR_Tools_Example.IO;

namespace DjnIndustries.HR_Tool_Example.Logging
{
    public class Logger : ILogger
    {
        #region Interfaces
        // Variable for saving the path
        private readonly IDataHandler _fileHandler;
        #endregion

        #region Ctor
        /// <summary>
        /// This constructor created a logger instance
        /// </summary>
        /// <param name="path">Path to the folder where logs should be written in</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Logger(IDataHandler fileHandler)
        {
            // Check if file handler is null
            if (fileHandler == null)
                throw new ArgumentException(nameof(fileHandler));

            // Store file handler interface
            _fileHandler = fileHandler;
        }
        #endregion

        #region Interface - ILogger
        /// <summary>
        /// Logs a message to a file
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            // Append to the file using the file handler interface
            // and insert a time mark with a sortable date and time
            // String interpolation finds its use in here as well.
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
            _fileHandler.Append($"{DateTime.Now:s} - {message}\n");
        }
        #endregion
    }
}