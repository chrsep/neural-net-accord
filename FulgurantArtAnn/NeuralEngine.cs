using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Accord.Imaging.Converters;
using AForge.Imaging.Filters;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace FulgurantArtAnn
{
    /// <summary>
    ///     Neural Network Singletion, Handles all neural network and data related task
    ///     Example: Training, save data, recognize image, etc
    /// </summary>
    internal class NeuralEngine
    {
        private static NeuralEngine _instance;
        private readonly ImageToArray _imageToArray;
        private Dictionary<string, double[][]> _allData;
        private readonly ActivationNetwork _classificationNetwork;
        private readonly DistanceNetwork _clusteringNetwork;

        private NeuralEngine()
        {
            _classificationNetwork = File.Exists("BPNNBrain.net")
                ? Network.Load("BPNNBrain.net") as ActivationNetwork
                : new ActivationNetwork(new SigmoidFunction(), 100, 10, 1);

            _clusteringNetwork = File.Exists("SOMBrain.net")
                ? Network.Load("SOMBrain.net") as DistanceNetwork
                : new DistanceNetwork(100, 100);

            _imageToArray = new ImageToArray(0, 1);
            _allData = GetTrainingData();
        }

        //PUBLIC FUNCTIONS==================================================================
        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());

        /// <summary>
        ///     Trains Network for classification (Activation Network, Backpropagation)
        /// </summary>
        /// <param name="epoch">Number of epoch</param>
        /// <returns>Neural network Error</returns>
        public double TrainClasificationNetwork(int epoch = 10000)
        {
            var dataArray = _allData.Values.ToArray();
            var input = new List<double[]>();
            var output = new List<double[]>();
            for (var i = 0; i < dataArray.Length; i++)
            {
                input.AddRange(dataArray[i]);
                output.AddRange(dataArray[i].Select(data => new double[] {i}));
            }
            output = NormalizeOutput(output);
            var trainer = new BackPropagationLearning(_classificationNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = trainer.RunEpoch(input.ToArray(), output.ToArray());

            // BUG: Error hovers around 0.4, cant go lower
            return error;
        }

        /// <summary>
        ///     Trains Network for Clustering (Distance Network, Self Organizing Map)
        /// </summary>
        /// <param name="epoch">Number of epoch</param>
        /// <returns>Neural network Error</returns>
        // TODO: Build the correct clustering network
        public void ClusterData(Bitmap bitmap, int epoch = 10000)
        {
           
        }

        /// <summary>
        ///     Used to classify image into a category
        /// </summary>
        /// <param name="processedImage">Image that has been preprocessed</param>
        /// <returns>Category that the image belongs to (according to the network)</returns>
        public string Classify(Bitmap processedImage)
        {
            double[] array;
            _imageToArray.Convert(processedImage, out array);
            var result = (int)_classificationNetwork.Compute(array)[0]*_allData.Count;
            return _allData.Keys.ToArray()[result];
        }

        /// <summary>
        ///     Save network to files
        /// </summary>
        public void Save()
        {
            _clusteringNetwork.Save("SOMBrain.net");
            _classificationNetwork.Save("BPNNBrain.net");
        }

        /// <summary>
        ///     Preprocessed image (Grayscale, Threshold, Reduce Noice / Crop,Resize)
        /// </summary>
        /// <param name="image">Image to be processed</param>
        /// <returns>Processed image (10x10, black and white image)</returns>
        public static Bitmap PreprocessImage(Bitmap image)
        {
            image = Grayscale.CommonAlgorithms.RMY.Apply(image);
            image = new Threshold(127).Apply(image);
            image = Crop(image);
            return new ResizeBilinear(10, 10).Apply(image);
        }

        /// <summary>
        /// Check whether neural engine singleton instance already exists
        /// </summary>
        /// <returns>True if Neural Engine singleton alreade exists, false otherwise</returns>
        public static bool IsExist() => _instance != null;

        /// <summary>
        ///     Quick function to turn list of image paths into
        ///     preprocessed image that has been converted into array, ready for training
        /// </summary>
        /// <param name="filePaths">Array of file paths</param>
        /// <returns>Array of images in the form of array</returns>
        private double[][] PreprocessImageFromFiles(string[] filePaths)
        {
            var result = new double[filePaths.Length][];
            for (var i = 0; i < filePaths.Length; i++)
                // TODO: Find out wether to use imagetoarray or to use denormalization method
                result[i] = NormalizeInput(PreprocessImage(new Bitmap(filePaths[i])));
            return result;
        }

        /// <summary>
        ///     Gets all of images from picture folder
        /// </summary>
        /// <returns>Dictionary of category name and image array</returns>
        public static Dictionary<string, List<Bitmap>> GetImages() =>
            Directory.GetDirectories("pictures").ToDictionary(
                path => new DirectoryInfo(path).Name,
                path => Directory.GetFiles(path).Select(file => new Bitmap(file)).ToList());

        public void ReloadData()
        {
            _allData = GetTrainingData();
        }


        // PRIVATE FUNCTIONS===================================================================================================

        /// <summary>
        ///     Normalized Output for training, Formula: output/total number of category
        /// </summary>
        /// <param name="outputs">Array of output to be normalized</param>
        /// <returns>Normalized Output</returns>
        private List<double[]> NormalizeOutput(List<double[]> outputs) =>
            outputs.Select(output => new[] {output[0]/_allData.Count}).ToList();

        /// <summary>
        ///     Reduce image size to only contain the important information (Part of image that has content above threshold)
        /// </summary>
        /// <param name="image">Image to Crop</param>
        /// <returns>Cropped image</returns>
        private static Bitmap Crop(Bitmap image)
        {
            int xMin = image.Width,
                yMin = image.Height,
                xMax = 0,
                yMax = 0;

            for (var i = 0; i < image.Width; i++)
                for (var j = 0; j < image.Height; j++)
                {
                    if (image.GetPixel(i, j).R <= 127) continue;
                    xMin = i < xMin ? i : xMin;
                    yMin = j < yMin ? j : yMin;
                    xMax = i > xMax ? i : xMax;
                    yMax = j > yMax ? j : yMax;
                }

            if (xMin == image.Width) xMin = 0;
            if (yMin == image.Height) yMin = 0;
            if (xMax == 0) xMax = image.Width;
            if (yMax == 0) yMax = image.Height;

            var cropped = new Bitmap(xMax, yMax);
            Graphics.FromImage(cropped).DrawImage(image, xMin, yMin, xMax, yMax);
            return cropped;
        }

        /// <summary>
        ///     Gets all of images from picture folder
        /// </summary>
        /// <returns>Dictionary of category name and image array</returns>
        private Dictionary<string, double[][]> GetTrainingData() =>
            Directory.GetDirectories("pictures").ToDictionary(
                path => new DirectoryInfo(path).Name,
                path => PreprocessImageFromFiles(Directory.GetFiles(path)));

        private double[] NormalizeInput(Bitmap image)
        {
            var inputNormal = new double[100];
            for (var i = 0; i < image.Width; i++)
            {
                for (var j = 0; j < image.Height; j++)
                {
                    int input = image.GetPixel(i, j).B / 255;
                    inputNormal[i + j] = input;
                }
            }

            return inputNormal;
        }
    }
}