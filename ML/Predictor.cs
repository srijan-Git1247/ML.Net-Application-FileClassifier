using System;
using System.IO;
using FileClassifer.ML.Objects;
using FileClassifier.ML.Base;
using FileClassifier.ML.Objects;
using Microsoft.ML;



namespace FileClassifier.ML
{
     public class Predictor:BaseML//Provides prediction support in our project
    {
        public void Predict(string inputDataFile)
        {
            if(!File.Exists(ModelPath))
            {
                ////Verifying if the model exists prior to reading it
                Console.WriteLine($"Failed to find model at {ModelPath}");
                return;

            }
            if(!File.Exists(inputDataFile))
            {
                //Verifying if the input file exists before making predictions on it 
                Console.WriteLine($"Failed to find input data at {inputDataFile}");
                return;
            }

            /*Loading the model  */
            //Then we define the ITransformer Object
            ITransformer mlModel;

            using (var stream =new FileStream(ModelPath, FileMode.Open, FileAccess.Read,FileShare.Read))
            {

                mlModel = MlContext.Model.Load(stream, out _);
                //Stream is disposed as a result of Using statement
            }
            if(mlModel==null)
            {
                Console.WriteLine("Failed to load the model");
                return;
            }
            // Create a prediction engine
            //Pass into FileInput and FilePrediction to the CreatePredicitionEngine Method
            var predictionEngine = MlContext.Model.CreatePredictionEngine<FileInput, FilePrediction>(mlModel);

            //Create the FileInput object, setting the Strings property with the return values of the GetStrings method
            // Call predict model on prediction engine class
            var prediction = predictionEngine.Predict(new FileInput
            {
                Strings = GetStrings(File.ReadAllBytes(inputDataFile))
            });

            //Update the output call to the console object with our file classification and probability

            Console.WriteLine($"Based on the file ({inputDataFile}) the file is classified as {(prediction.IsMalicious ? "malicious" : "benign")}" + $" at a confidence level of {prediction.Probability:P0}");





        }
    }
}
