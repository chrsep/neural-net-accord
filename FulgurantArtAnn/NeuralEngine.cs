using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Accord.Imaging.Converters;
using AForge.Imaging.Filters;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Accord.Statistics.Analysis;

namespace FulgurantArtAnn
{
    /// <summary>
    ///     Neural Network Singletion, Handles all neural network and data related task
    ///     Example: Training, save data, recognize image, etc..
    /// </summary>
    internal class NeuralEngine
    {
        private static NeuralEngine _instance;
        private readonly ImageToArray _imageToArray;
        private Dictionary<string, double[][]> _allData;
        private ActivationNetwork _classificationNetwork;
        private DistanceNetwork _clusteringNetwork;

        private NeuralEngine()
        {
            _allData = GetTrainingData();
            _imageToArray = new ImageToArray(0, 1);

            _classificationNetwork = File.Exists("BPNNBrain.net")
                ? Network.Load("BPNNBrain.net") as ActivationNetwork
                : new ActivationNetwork(new SigmoidFunction(), 100, 10, 1);

            _clusteringNetwork = File.Exists("SOMBrain.net")
                ? Network.Load("SOMBrain.net") as DistanceNetwork
                : CreateNewDistanceNetwork();
        }

        //PUBLIC FUNCTIONS==================================================================
        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());

        /// <summary>
        /// Check whether neural engine singleton instance already exists
        /// </summary>
        /// <returns>True if Neural Engine singleton alreade exists, false otherwise</returns>
        public static bool IsExist() => _instance != null;

        /// <summary>
        ///     Trains Network for classification (Activation Network, Backpropagation)
        /// </summary>
        /// <param name="epoch">Number of epoch</param>
        /// <returns>Neural network Error</returns>
        public double TrainClasificationNetwork(int epoch = 10000)
        {
            _classificationNetwork = new ActivationNetwork(new SigmoidFunction(), 100, 10, 1);
            var dataArray = _allData.Values.ToArray();
            var datum = ShuffleData(dataArray);
            var trainer = new BackPropagationLearning(_classificationNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = trainer.RunEpoch(datum.Select(data => data.Value).ToArray(), NormalizeOutput(datum.Select(data => data.Key).ToList()).ToArray());

            // BUG: Error hovers around 0.4, cant go lower
            return error;
        }

        /// <summary>
        /// Shuffle data around and reorganize it into keyvaluepair(output, input).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, double[]>> ShuffleData(IEnumerable<IEnumerable<double[]>> input)
        {
            var random = new Random();
            var result = new List<KeyValuePair<int, double[]>>();
            for (var i = 0; i < input.Count(); i++)
                result.AddRange(input.ElementAt(i).ToList().Select(imageByCategory => new KeyValuePair<int, double[]>(i, imageByCategory)));
            var shuffledResult = result.OrderBy(unused => random.Next()); ;
            for (var i = 0; i < 8; i++)
                shuffledResult = shuffledResult.OrderBy(unused => random.Next());
            
            return shuffledResult.ToList();
        }

        /// <summary>
        ///     Trains Network for Clustering (Distance Network, Self Organizing Map)
        /// </summary>
        /// <param name="epoch">Number of epoch</param>
        /// <returns>Neural network Error</returns>
        // TODO: Build the correct clustering network
        public double TrainClusteringNetwork(int epoch = 10000)
        {
            _clusteringNetwork = CreateNewDistanceNetwork();
            var pcaResult = ComputePca(RemoveFromCategory(_allData.Values.ToList()));
            var trainer = new SOMLearning(_clusteringNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
            {
                error = trainer.RunEpoch(pcaResult);
                if (error < 0.0001)
                    break;
            }

            return error;
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
            var computedValue = _classificationNetwork.Compute(array)[0];
            var result = (int) Math.Round(computedValue * (_allData.Count - 1)) ;
            return _allData.Keys.ToArray()[result ];
        }

        /// <summary>
        /// Find a similar image of the image inputted into the method
        /// </summary>
        /// <param name="inputFilePath">full path to the image file</param>
        /// <returns>List of bitmap similar to the input, empty bitmap list if no image is similar</returns>
        public List<Bitmap> FindSimilar(string inputFilePath)
        {
            TrainClasificationNetwork();
            var categories = _allData.Keys.ToList();
            var pathImageDictionary = new Dictionary<string, Bitmap>();

            categories.ForEach(
                category => Directory.GetFiles("pictures/" + category).ToList().ForEach(
                    file => pathImageDictionary.Add(file, new Bitmap(file))
                )
            );

            List<KeyValuePair<string, double[]>> preprocessedImageArray = pathImageDictionary.Select(data =>
            {
                double[] imageArray;
                _imageToArray.Convert(PreprocessImage(data.Value), out imageArray);
                return new KeyValuePair<string, double[]>(data.Key, imageArray);
            }).ToList();

            int chosenImageCluster = 0, i =0;
            var pca = ComputePca(preprocessedImageArray.Select(data => data.Value).ToList());
            var clusterImageDictionary = preprocessedImageArray.Select(pair =>
            {
                _clusteringNetwork.Compute(pca[i]);
                var cluster = _clusteringNetwork.GetWinner();
                if (pair.Key.Equals(inputFilePath))
                {
                    chosenImageCluster = cluster;
                }
                i++;
                return new KeyValuePair<string, int>(pair.Key, cluster);
            });

            return
                clusterImageDictionary.Where(data => data.Value == chosenImageCluster)
                    .Select(data => new Bitmap(data.Key)).ToList();
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
            //image = Grayscale.CommonAlgorithms.RMY.Apply(image);
            // = new Threshold(127).Apply(image);
            //image = Crop(image);
            return new ResizeBilinear(10, 10).Apply(image);
        }


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
        private List<double[]> NormalizeOutput(List<int> outputs)=>
            outputs.Select(output => new[] { ((double) output) / _allData.Count }).ToList();

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
                    var input = (double) image.GetPixel(i, j).B/255;
                    inputNormal[i + j] = input;
                }
            }

            return inputNormal;
        }

        private DistanceNetwork CreateNewDistanceNetwork()
        {
            var numberOfCategory = _allData.Count;
            var result = 2;
            while (result*result < numberOfCategory)
            {
                result++;
            }
            var arrayOfAllImages = new List<double[]>();
            _allData.Values.ToList().ForEach(images => arrayOfAllImages.AddRange(images));
            return new DistanceNetwork(arrayOfAllImages.Count, result*result);
        }

        private List<T> RemoveFromCategory<T>(IEnumerable<IEnumerable<T>> input)
        {
            var result = new List<T>();
            input.ToList().ForEach(data => result.AddRange(data));
            return result.ToList();
        }

        private double[][] ComputePca(List<double[]> input)
        {
            var pca = new PrincipalComponentAnalysis(input.ToArray());
            pca.Compute();
            var pcaResult = new double[pca.Result.GetLength(0)][];
            for (int i = 0; i < pca.Result.GetLength(0); i++)
            {
                pcaResult[i] = new double[pca.Result.GetLength(1)];
                for (int j = 0; j < pca.Result.GetLength(1); j++)
                {
                    pcaResult[i][j] = pca.Result[i, j];
                }
            }
            return pcaResult;
        }
    }
}