The FeatureExtractor Class provides feature extraction for the given folder of files. Once the extraction is complete, the classification and strings data is written out to the "sampledata" file.

The sampledata.csv attached file in the project contains 8 rows of random data. Each of these rows contains two columns worth of data. The first is classification, with true being "malicious" and false 
being "benign". These properties are mapped in the FileInput Class.

![image](https://github.com/srijan-Git1247/ML.Net-Application-FileClassifier/assets/73276238/1d6f96ae-57f9-4489-a861-b456016ffea2)


The trainer used in the application uses SDCA using the logistic regression variation.


*Note: If you are looking for sample files, the c:\Windows and c:\Windows\System32 folders contain numerous Windows Executables and DLLS. In addition, if you are looking to create malicious-looking files
that are actually clean, you can create files on the fly on http://cwg.io in various file formats.


Run the Console Application with commandline arguments:

1. Assuming the folder of files called "data_files" exists, execute the following command:
   data files

>D:\Machine Learning Projects\FileClassifier\bin\Debug\net8.0\FileClassifier.exe extract data-files
> Extracted 8 files to sampledata.csv

2. Train the model using the sampledata.csv
> >D:\Machine Learning Projects\FileClassifier\bin\Debug\net8.0\FileClassifier.exe train D:\Machine Learning Projects\FileClassifier\Data\sampledata.csv

A FileClassifier model will be created in then folder D:\Machine Learning Projects\FileClassifier\bin\Debug\net8.0\


3. Run Prediction on the newly trained model using thw compiled FileClassifier.exe
> predict "D:\Machine Learning Projects\FileClassifier\bin\Debug\net8.0\FileClassifier.exe"
> Based on the file (D:\Machine Learning Projects\FileClassifier\bin\Debug\net8.0\FileClassifier.exe) the file is classified as benign at a confidence level of 4%
