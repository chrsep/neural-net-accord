using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FulgurantArtAnn
{
    class NeuralEngine 
    {
        private static NeuralEngine _instance;

        private NeuralEngine(){}

        public static NeuralEngine Instance => _instance ?? (_instance = new NeuralEngine());
    }
}
