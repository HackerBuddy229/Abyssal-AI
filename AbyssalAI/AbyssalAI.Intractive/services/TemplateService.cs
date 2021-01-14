using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AbyssalAI.Interactive.models;
using static System.String;

namespace AbyssalAI.Interactive.services
{
    public class TemplateService
    {
        private readonly Uri _templateLocation = new Uri(Path.GetFullPath("templates/"));

        public IList<INetworkTemplate> GetTemplates()
        {

            //get file names
            var templatesUris = Directory.EnumerateFiles(_templateLocation.AbsolutePath);
            var uris = templatesUris as string[] ?? 
                       templatesUris.Where(x => x.Split(".").Any(y => y == "json")).ToArray();

            if (!uris.Any())
                return null;

            var output = new List<INetworkTemplate>();

            foreach (var uri in uris)
            {
                var json = File.ReadAllText(uri);
                if (IsNullOrWhiteSpace(json))
                    continue;

                var template = JsonSerializer.Deserialize<CurrencyDataNetworkTemplate>(json);
                output.Add(template);
                //fetch and deserialize files
            }


            return output;
            //return
        }

        public void CreateTemplate(INetworkTemplate template)
        {
            //serialize to json
            var json = JsonSerializer.Serialize(template);

            //filename
            var filename = Empty;
            var filenameComponents = template.Title.Split(" ").ToList();
            filenameComponents.ForEach(x => filename += x);

            //get stream
            var uri = _templateLocation.AbsolutePath + filename + ".json";

            using var fs = new FileStream(uri, FileMode.Create);
            using var sw = new StreamWriter(fs);
                sw.Write(json);
        }



    }
}
