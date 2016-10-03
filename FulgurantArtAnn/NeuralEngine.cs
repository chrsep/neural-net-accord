using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace FulgurantArtAnn
{
    class NeuralEngine 
    {
        private static NeuralEngine _instance;
        private SOMLearning _somTrainer;
        private BackPropagationLearning _bpTrainer;

        private NeuralEngine()
        {
            DistanceNetwork distanceNetwork;
            ActivationNetwork activationNetwork;

            try
            {
                activationNetwork = Network.Load("BPNNBrain.net") as ActivationNetwork;
                distanceNetwork = Network.Load("SOMBrain.net") as DistanceNetwork;
            }
            catch (FileNotFoundException e)
            {
                activationNetwork = new ActivationNetwork(new SigmoidFunction(), 100, 100);
                distanceNetwork = new DistanceNetwork(100, 100);
            }
            

            _somTrainer = new SOMLearning(distanceNetwork);
            _bpTrainer = new BackPropagationLearning(activationNetwork);
        }

        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());
    }
}
