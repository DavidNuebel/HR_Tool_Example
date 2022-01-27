namespace DjnIndustries.HR_Tools_Example.IO
{
    /// <summary>
    /// Class for demonstration of project structure.
    /// Goal is to achieve independence and avoid circular dependencies.
    ///
    /// This class handles file operations for the entire application.
    /// Due to dependency injection this could be swapped by an other handler
    /// that writes its messages to a database or some other technology that
    /// can store data persistently
    /// </summary>
    public class FileHandler : IDataHandler
    {
        #region Fields
        /// <summary>
        /// File path that will be used for file operations
        /// </summary>
        private readonly string _filePath;
        #endregion

        #region Ctor
        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath">File path that should be used for file operations</param>
        /// <exception cref="FileNotFoundException"></exception>
        public FileHandler(string filePath)
        {
            // Store the file path
            _filePath = filePath;
        }
        #endregion

        #region Interface - IDataHandler
        /// <summary>
        /// This method makes sure that the file is created
        /// </summary>
        /// <returns><c>true</c> if the file exists, <c>false if not</c></returns>
        public bool Validate()
        {
            // Check if parameter is not empty
            if (string.IsNullOrEmpty(_filePath))
                throw new ArgumentNullException(nameof(_filePath));

            // Check whether the directory exists or not
            // if not, try to create it
            if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
            {
                try // Try to create the directory
                {
                    // Check if the base name of the path is not empty
                    var directory = Path.GetDirectoryName(_filePath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        // Create the directory as the base path is valid
                        Directory.CreateDirectory(directory);
                    }
                }
                catch (Exception ex) // Catches any exception occurring in the try block
                {
                    return false; // Path couldn't be created, is invalid or worse, because an exception was thrown
                }
            }

            if (!File.Exists(_filePath))
            {
                try // Try to create the file
                {
                    // Check if the base name of the path is not empty
                    var fileName = Path.GetFileName(_filePath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        // Return false as the fileName is empty
                        return false;
                    }
                    else
                    {
                        // Create the file as the file name is valid
                        // The base path was checked before and
                        // don't need attention here
                        File.Create(_filePath).Close();
                    }
                }
                catch (Exception ex) // Catches any exception occurring in the try block
                {
                    return false; // File couldn't be created, is invalid or worse, because an exception was thrown
                }
            }

            // Everything is fine.
            // The directory exists and the file too.
            // Ready to read or write
            return true;
        }

        /// <summary>
        /// This method will check the file and path and then override a file with content.
        /// </summary>
        /// <param name="content">Content that should be written</param>
        public void Write(string content)
        {
            // Check validity of file
            if (!Validate())
                return; // Exit method

            // Check validity of parameter
            if (string.IsNullOrEmpty(content))
                return;  // Exit method

            try // Try to write content to file
            {
                File.WriteAllText(_filePath, content);
            }
            catch (Exception ex)
            {
                // Leave empty and return later.
            }

            return; // Exit method
        }

        /// <summary>
        /// This method will check the file and path and then append the content to a file.
        /// </summary>
        /// <param name="content">Content that should be written</param>
        public void Append(string content)
        {
            // Check validity of file
            if (!Validate())
                return; // Exit method

            // Check validity of parameter
            if (string.IsNullOrEmpty(content))
                return;  // Exit method

            try // Try to append content to file
            {
                File.AppendAllText(_filePath, content);
            }
            catch (Exception ex)
            {
                // Leave empty and return later.
            }

            return; // Exit method
        }

        /// <summary>
        /// Reads out a file and returns the lines
        /// </summary>
        /// <returns>Lines of data</returns>
        public string? Read()
        {
            if (!Validate())
                return null; // File is not valid

            try // Try to read from the file
            {
                return File.ReadAllText(_filePath);
            }
            catch (Exception ex) // Catches any exception occurring in the try block
            {
                return null; // Couldn't read file
            }
        }
        #endregion
    }
}