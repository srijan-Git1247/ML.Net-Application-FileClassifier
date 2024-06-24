using Microsoft.ML.Data;



//This class provides the container for the trained classification and the strings data we extract.
namespace FileClassifier.ML.Objects
{
    public class FileInput
    {
        [LoadColumn(0)]
        public bool Label
        {
            get;
            set;
        }

        [LoadColumn(1)]
        public string? Strings
        { get;  
          set;

        }
    
    
    }

}