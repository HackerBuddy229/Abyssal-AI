using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Core.models;
using AbyssalAI.Interactive.services;

namespace AbyssalAI.Interactive.models
{
    public class ApplicationState
    {
        public ConcurrentBag<EpochResult> _trainingStatus;
        public INetworkTemplate SelectedTemplate { get; set; }

        public INeuralNetwork NeuralNetwork { get; set; }


        public ConcurrentBag<EpochResult> TrainingStatus
        {
            get => _trainingStatus;
            set => _trainingStatus = value;
        }

        public NetworkTrainingResult TrainingResult { get; set; }

        public bool IsTraining => TrainingStatus != null && TrainingStatus.Any();
    }
}
