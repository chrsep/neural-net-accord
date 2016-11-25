using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Accord.Imaging.Converters;
using AForge.Imaging.Filters;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace FulgurantArtAnn
{
    class NeuralEngine
    {
        private static NeuralEngine _instance;
        private readonly DistanceNetwork _clusteringNetwork;
        private readonly ActivationNetwork _classificationNetwork;
        private Dictionary<String, double[][]> _allData;

        private NeuralEngine()
        {
            try
            {
                _classificationNetwork = Network.Load("BPNNBrain.net") as ActivationNetwork;
                _clusteringNetwork = Network.Load("SOMBrain.net") as DistanceNetwork;
            }
            catch (Exception)
            {
                _classificationNetwork = new ActivationNetwork(new SigmoidFunction(), 100, 100, 1);
                _clusteringNetwork = new DistanceNetwork(100, 100);
            }
            Reload();
        }

        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());

        public double[][] PreprocessImageFromFiles(string[] files)
        {
            var imageToArrayConverter = new ImageToArray(min: -1, max: +1);
            var grayscale = new Grayscale(0, 0, 0);
            var threshold = new Threshold();
            var scaling = new ResizeBicubic(10, 10);
            var result = new double[files.Length][];
            var i = 0;
            foreach (var file in files)
            {
                var image = Image.FromFile(file) as Bitmap;
                image = grayscale.Apply(image);
                image = threshold.Apply(image);
                image = scaling.Apply(image);
                imageToArrayConverter.Convert(image, out result[i]);
                i++;
            }
            return result;
        }

        public double TrainSimilarityNetwork(double[][] input, double[][] output, int epoch = 10000)
        {
            var bpTrainer = new BackPropagationLearning(_classificationNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = bpTrainer.RunEpoch(input, output);
            return error;
        }

        public double TrainRecognizerNetwork(double[][] input, int epoch = 10000)
        {
            var somTrainer = new SOMLearning(_clusteringNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = somTrainer.RunEpoch(input);
            return error;
        }

        public void Reload()
        {
            _allData = new Dictionary<string, double[][]>();
            var categories = Directory.GetDirectories("pictures");
            foreach (var category in categories)
            {
                var imagePaths = Directory.GetFiles(category);
                var images = PreprocessImageFromFiles(imagePaths);
                _allData.Add(new DirectoryInfo(category).Name, images);
            }
        }
    }
}