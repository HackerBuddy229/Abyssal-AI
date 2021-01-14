using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Interactive.models;
using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;

namespace AbyssalAI.Interactive.services
{
    public class CsvFinancialSerializer: IDataSerializer<CurrencyData>
    {
        public IEnumerable<CurrencyData> SerializeData(string path)
        {
            using var stream = new StreamReader(path);
            using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);

            var output = csv.GetRecords<CurrencyData>();

            return output;
        }
    }
}
