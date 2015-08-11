using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IncludeDay.Services.Models
{
    [Serializable]
    public class PredioDTO
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Nome")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "Descricao")]
        public string Descricao { get; set; }
    }
}