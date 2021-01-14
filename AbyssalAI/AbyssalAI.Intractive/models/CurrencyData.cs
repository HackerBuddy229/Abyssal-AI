using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace AbyssalAI.Interactive.models
{
    public class CurrencyData
    {
        public string Date { get; set; }
        public DateTime ParsedDateTime => DateTime.Parse(Date, CultureInfo.InvariantCulture);

        public float Open { get; set; }

        public float High { get; set; }
        public float Low { get; set; }

        [Name("Close*")]
        public float Close { get; set; }

        [Name("Adj Close**")]
        public float AdjClose { get; set; }
    }
}
