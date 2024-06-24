using System;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FileClassifier.Common;
using Microsoft.ML;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
namespace FileClassifier.ML.Base
{
    public class BaseML
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Constant.MODEL_FILENAME);
        protected readonly MLContext MlContext;

        private static Regex? _stringRex;

     
        protected BaseML()
        {
            // In this constructor, we intialize the stringRex variable to the regular expressions we will use to extract strings.

            MlContext = new MLContext(2024);
            //Encoding.RegisterProvider is critical to utilize the Windows-1252 encoding
            //This encoding is the encoding Windows Executable Utilizes.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _stringRex = new Regex(@"[ -~\t]{8,}", RegexOptions.Compiled);
        }
        protected string GetStrings(byte[] data)
        {
            // This method takes the bytes, runs the previously created compiled regular expresion, and extracts the string matches
            var stringLines = new StringBuilder();

            //Sanity check the input data is not null or empty
            if(data== null || data.Length == 0)
            {
                return stringLines.ToString();
            }

            using (var ms= new MemoryStream(data,false))
            {
                using (var streamReader = new StreamReader(ms, Encoding.GetEncoding(1252), false, 2028, false))
                {

                    while (!streamReader.EndOfStream)//Loop through the SR object until EOF is reached
                    {
                        var line = streamReader.ReadLine();

                        //Apply some string clean up of the data and handle whether the line is empty or not gracefully
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        line = line.Replace("^", "").Replace(")", "").Replace("-", "");

                        //Append the regular expression matches and append those matches to the previously defined stringLines variable
                        stringLines.Append(string.Join(string.Empty, _stringRex.Matches(line).Where(a => !string.IsNullOrEmpty(a.Value) && !string.IsNullOrWhiteSpace(a.Value)).ToList()));
                    }
                }
            }
            return string.Join(string.Empty, stringLines);
        }
    }
}
