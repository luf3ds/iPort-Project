using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace WpfHackthon.Classes
{
    public class JsonService
    {
        public List<Container> CarregarDeArquivo(string caminho)
        {
            string conteudoJson = File.ReadAllText(caminho);
            dynamic obj = JsonConvert.DeserializeObject(conteudoJson);
            string data = obj.data.ToString();
            return JsonConvert.DeserializeObject<List<Container>>(data) ?? new List<Container>();
        }

        public void SalvarEmArquivo(string caminho, List<Container> containers, int bays, int rows, int tiers)
        {
            JObject obj;
            if (File.Exists(caminho))
            {
                obj = JObject.Parse(File.ReadAllText(caminho));
                obj["data"] = JToken.FromObject(containers);
            }
            else
            {
                obj = new JObject(
                    new JProperty("bays", bays),
                    new JProperty("rows", rows),
                    new JProperty("tiers", tiers),
                    new JProperty("data", JToken.FromObject(containers))
                );
            }
            File.WriteAllText(caminho, obj.ToString(Formatting.Indented));
        }
    }
}