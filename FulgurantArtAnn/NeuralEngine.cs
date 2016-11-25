using System;
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
    internal class NeuralEngine
    {
        private static NeuralEngine _instance;
        private Dictionary<string, double[][]> _allData;
        private ActivationNetwork _classificationNetwork;
        private DistanceNetwork _clusteringNetwork;

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
        }

        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());

        public double[][] PreprocessImageFromFiles(string[] files)
        {
            var imageToArrayConverter = new ImageToArray(-1, +1);
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

        public double TrainClasificationNetwork(int epoch = 10000)
        {
            Reload();
            var dataArray = _allData.Values.ToArray();
            var input = new List<double[]>();
            var output = new List<double[]>();
            for (int i = 0; i < dataArray.Length; i++)
            {
                input.AddRange(dataArray[i]);
                foreach (var data in dataArray[i])
                {
                    var temp = new double[1];
                    temp[0] = i;
                    output.Add(temp);
                }
            }

            var bpTrainer = new BackPropagationLearning(_classificationNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = bpTrainer.RunEpoch(input.ToArray(), output.ToArray());
            return error;
        }

        public double TrainClusteringNetwork(int epoch = 10000)
        {
            Reload();
            var dataArray = _allData.Values.ToArray();
            var input = new List<double[]>();
            foreach (var data in dataArray)
            {
                input.AddRange(data);

            }

            var somTrainer = new SOMLearning(_clusteringNetwork);
            var error = 0d;
            for (var i = 0; i < epoch; i++)
                error = somTrainer.RunEpoch(input.ToArray());
            return error;
        }

        public void Reload()
        {
            _allData = new Dictionary<string, double[][]>();

            var paths = Directory.GetDirectories("pictures");
            foreach (var path in paths)
            {
                var imagePaths = Directory.GetFiles(path);
                var images = PreprocessImageFromFiles(imagePaths);
                _allData.Add(new DirectoryInfo(path).Name, images);
            }
        }
    }
}