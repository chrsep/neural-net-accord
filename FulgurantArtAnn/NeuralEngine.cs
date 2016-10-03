using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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
        private DistanceNetwork _distanceNetwork;
        private ActivationNetwork _activationNetwork;

        private NeuralEngine()
        {
            var imageToArrayConverter = new ImageToArray(min: -1, max: +1);
            var grayscale = new Grayscale(0, 0, 0);
            var scaling = new ResizeBicubic(10, 10);
            double[][] data = new double[][] {};
            try
            {
                _activationNetwork = Network.Load("BPNNBrain.net") as ActivationNetwork;
                _distanceNetwork = Network.Load("SOMBrain.net") as DistanceNetwork;
            }
            catch (FileNotFoundException)
            {
                _activationNetwork = new ActivationNetwork(new SigmoidFunction(), 100, 100, 1);
                _distanceNetwork = new DistanceNetwork(100, 100);
            }

            try
            {
                var files = Directory.GetFiles("images/", "*.jpg", SearchOption.AllDirectories);
                var i = 0;
                data = new double[files.Length][];
                foreach (var file in files)
                {
                    double[] pixelArray;
                    var image = Image.FromFile(file) as Bitmap;
                    image = grayscale.Apply(image);
                    image = scaling.Apply(image);
                    imageToArrayConverter.Convert(image, out data[i]);
                    i++;
                }
            }catch(DirectoryNotFoundException) { }

            var bpnn = new BackPropagationLearning(_activationNetwork);
            int epoch = 1000000;
            double desiredError = 0.01;
            var finalerror = 0d;
            for (int i = 0; i < epoch; i++)
            {
                double error =
                    bpnn.RunEpoch(data, data);

                if (error < desiredError)
                    break;

                if (i%200000 == 0)
                    finalerror = error;
            }
            MessageBox.Show(finalerror.ToString(CultureInfo.InvariantCulture));
        }

        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());


    }
}
