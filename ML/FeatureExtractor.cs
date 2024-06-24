using System;
using System.IO;

using FileClassifier.Common;
using FileClassifier.ML.Base;


//This class provides our feature extraction for the given folder of our files.
//Once the extraction is complete, the classification and strings data is written out to the sampledata file.

namespace FileClassifier.ML
{
    public class FeatureExtractor: BaseML
    {
        public void Extract(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            using (var streamWriter = new StreamWriter(Path.Combine(AppContext.BaseDirectory, $"D:\\Machine Learning Projects\\FileClassifier\\Data\\{Constant.SAMPLE_DATA}")))
            {
                foreach (var file in files)
                {
                    var strings= GetStrings(File.ReadAllBytes(file));
                    streamWriter.WriteLine($"{file.ToLower().Contains("malicious")}\t{strings}");
                }
            }
            Console.WriteLine($"Extracted {files.Length} to {Constant.SAMPLE_DATA}");

        }
    }
}