using Newtonsoft.Json;

namespace GetQuote2
{
  class Quote
  {
    [JsonProperty("a_url")]
    public string AUrl { get; set; }

    [JsonProperty("qt")]
    public string Qt { get; set; }

    [JsonProperty("q_url")]
    public string QUrl { get; set; }

    [JsonProperty("q_id")]
    public int QId { get; set; }

    [JsonProperty("an")]
    public string An { get; set; }
  }
}