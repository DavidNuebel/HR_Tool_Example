using DjnIndustries.HR_Tool_Example.Logging;
using DjnIndustries.HR_Tools_Example.IO;
using System.Text.Json;

namespace DjnIndustries.HR_Tool_Example.DataPersistence
{
    /// <summary>
    /// This class handles data storage for this application.
    /// It serializes incoming data into JSON format using the
    /// JSON serializer.
    /// More info down below
    /// -> https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview?pivots=dotnet-6-0
    /// </summary>
    public class PersistenceAgent : IPersistenceAgent
    {
        #region Interfaces
        /// <summary>
        /// Variable containing the reference to the logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Reference to a class that handles data storage
        /// </summary>
        private readonly IDataHandler _dataHandler;
        #endregion

        #region Ctor
        /// <summary>
        /// This constructor sets up the persistence agent.
        /// Therefore the logger and the path were passed in
        /// </summary>
        public PersistenceAgent(ILogger logger, IDataHandler dataHandler)
        {
            // Check if logger is null
            //if (_logger == null)
            //    throw new ArgumentNullException(nameof(logger));

            // Save logger reference in this class
            _logger = logger;

            // Check if data handler is null
            if (dataHandler == null)
                throw new ArgumentNullException(nameof(_dataHandler));

            // Save data handler interface in this class
            _dataHandler = dataHandler;
        }
        #endregion

        #region Interface - IPersistenceAgen
        /// <summary>
        /// Loads data of a given type from a file
        /// </summary>
        /// <typeparam name="T">Type of data that should be loaded</typeparam>
        /// <param name="path">File path</param>
        /// <returns></returns>
        public T Load<T>()
        {
            // Log event
            _logger.Log($"Loading data");

            // Get file content
            var fileContent = _dataHandler.Read();

            // Check if the file content is not empty
            if (string.IsNullOrEmpty(fileContent))
            {
                // Log event
                _logger.Log($"No data could be found in file");

                // return default value: null
                return default;
            }

            try // Try to deserialize the file content
            {
                // Deserialize the files content
                var obj = JsonSerializer.Deserialize(fileContent, typeof(T));

                // Check if deserialized object is null
                if (obj == null)
                    return default; // return default value: null

                // Return casted object
                return (T)obj;
            }
            catch (Exception ex) // Catches every exception that is thrown in the try block
            {
                // Log event
                _logger.Log($"Data could not be deserialized");
            }

            // return default value: null
            return default;
        }

        /// <summary>
        /// Updates a file with a given collection of items
        /// </summary>
        /// <typeparam name="T">Type of the item that should be updated</typeparam>
        /// <param name="item">Item that should be updated</param>
        public void Update<T>(T item)
        {
            // Log event
            _logger.Log($"Updating data");

            try // Try to serialize data and let the data handler write it to some storage
            {
                // Serialize the given item into JSON
                var content = JsonSerializer.Serialize(item, typeof(T));

                // Write the serialized data to data storage
                _dataHandler.Write(content);
            }
            catch (Exception ex) // Catches every exception that is thrown in the try block
            {
                // Log event
                _logger.Log($"Data coudn't be updated -> {ex.Message}");
            }

            // Log event
            _logger.Log($"Updated data");
        }
        #endregion
    }
}