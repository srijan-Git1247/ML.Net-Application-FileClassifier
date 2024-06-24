using Microsoft.ML.Data;


//This class provides the container for the classification, probability and score:
namespace FileClassifer.ML.Objects
{
    public class FilePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsMalicious
        {
            get;
            set;
        }
        public float Probability
        {
            get;
            set;
        }
    }
}