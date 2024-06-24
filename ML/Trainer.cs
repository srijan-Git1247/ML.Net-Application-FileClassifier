using System;
using System.IO;
using FileClassifier.ML.Base;
using FileClassifier.ML.Objects;
using Microsoft.ML;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FileClassifier.ML
{
    public class Trainer : BaseML
    {
        public void Train(string trainingFileName)
        {
           // Check if training data exists
            if (!File.Exists(trainingFileName)) 
            {
                Console.WriteLine($"Failed to find the training data file {trainingFileName}");
                return;
            }

            //Loads Text file into an IDataViewObject
            var trainingDataView = MlContext.Data.LoadFromTextFile<FileInput>(trainingFileName);

            //Creates Test Set from main Training Data
            //The parameter testFraction specifies the percentage of the dataset to hold back for testing in our case by 20%
            var dataSplit=MlContext.Data.TrainTestSplit(trainingDataView, testFraction:0.2);

            /*Creating pipeline 
             
             The FeaturizeText transforms build NGrams from the strings we extracted from the files.
            NGrams are a like breaking a long string into ranges of characters based on the value of NGram parameter.

            A bi-gram would take the sentence

            "Hello World" 
            and turn it into

            He-ll-oW-or-ld
             
             
             
             */




            var dataProcessPipeLine = MlContext.Transforms.CopyColumns("Label", nameof(FileInput.Label))
                                        .Append(MlContext.Transforms.Text.FeaturizeText("NGrams", nameof(FileInput.Strings)))
                                        .Append(MlContext.Transforms.Concatenate("Features", "NGrams"));



            //Create SDCA Trainer using the default paramters ("Label" and "Features")

            var trainer = MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features");
            //Complete our pipeline by appending the trainer we instantiated

            var trainingPipeLine = dataProcessPipeLine.Append(trainer);

            //Train the model with the data set created Earlier
            ITransformer trainedModel = trainingPipeLine.Fit(dataSplit.TrainSet);

            //Save created model to the filename specified matching training set's schema

            MlContext.Model.Save(trainedModel,dataSplit.TrainSet.Schema,ModelPath
                );






        }
    }

}