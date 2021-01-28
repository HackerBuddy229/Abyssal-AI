using System;
using System.Collections.Generic;
using System.Linq;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.helpers;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Interactive.services;

namespace AbyssalAI.Interactive.models
{
    public class CurrencyDataNetworkTemplate : INetworkTemplate
    {
        public string Title { get; set; }

        public string DataUri { get; set; }
        public float LearningRate { get; set; }
        public int MaxEpochs { get; set; }
        public int[] NetworkStructure { get; set; }

        public CurrencyDataNetworkTemplate(IDataSerializer<CurrencyData> dataSerializer, IDataDistributor dataDistributor)
        {
            _dataSerializer = dataSerializer;
            _dataDistributor = dataDistributor;
        }

        public CurrencyDataNetworkTemplate()
        {

            _dataSerializer = new CsvFinancialSerializer();
            _dataDistributor = new DataDistributor();
        }

        private readonly IDataSerializer<CurrencyData> _dataSerializer;
        private readonly IDataDistributor _dataDistributor;

        public IDataRation GetDataWindow()
        {
            var data = _dataSerializer.SerializeData(DataUri);
            var window = TranslateToDataWindows(data, 4);

            var ration = _dataDistributor.CreateDefaultDataRation(window);
            return ration;
        }

        private static IDataWindow[] TranslateToDataWindows(IEnumerable<CurrencyData> data, int windowSize)
        {
            var enumeratedData = data.ToList();
            var sizeOfOutput = enumeratedData.Count() / windowSize;
            var output = new IDataWindow[sizeOfOutput];

            for (var index = 0; index < sizeOfOutput; index++)
            {
                var window = new DataWindow();
                var inputLayer = enumeratedData
                    .Skip(windowSize*index)
                    .Take(windowSize).Select(x => x.Close)
                    .ToArray();

                var outputLayer = enumeratedData
                    .Skip(windowSize*index)
                    .Skip(windowSize).Take(1)
                    .Select(x => x.Close)
                    .Select(x => x > inputLayer[^1] ? 1.0F : 0.0F)
                    .ToArray();

                window.InputLayer = inputLayer;
                window.OutputLayer = outputLayer;
                output[index] = window;
            }

            return output;
        }
    }
}
