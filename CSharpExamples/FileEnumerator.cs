using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CSharpExamples
{
    public class FileEnumerator : IEnumerable<List<string>>
    {
        private readonly string _file;
        private readonly char _separator;

        public FileEnumerator(string file, char separator)
        {
            _file = file;
            _separator = separator;
        }

        public IEnumerator<List<string>> GetEnumerator()
        {
            var returnList = new List<string>();
            var previousFirstContentField = string.Empty;

            using (var streamReader = new StreamReader(_file))
            {
                // Loop through the file until we get to the end
                while (!streamReader.EndOfStream)
                {
                    // Read the next line and parse it.
                    var line = streamReader.ReadLine();
                    var lineContents = line.Split(_separator);

                    // Check if the first field matches the previous record
                    if (previousFirstContentField == lineContents[0])
                    {
                        returnList.Add(line);
                    }
                    else
                    {
                        // Current record doesn't match the previous
                        // Check if we have records to return.
                        // Only time we won't have record to return is the first line of the file.
                        if (returnList.Count > 0)
                        {
                            // Return what we have so far
                            yield return returnList;

                            // Back, so clear the list and add the new record
                            returnList.Clear();
                        }

                        // Set our previous field and add to the list
                        previousFirstContentField = lineContents[0];
                        returnList.Add(line);
                    }
                }

                // Reached the end of the file, so return our last set of results.
                yield return returnList;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
