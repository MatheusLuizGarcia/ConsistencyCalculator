using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsistencyCalculator.Core.Import
{
    public class YGOProDeckResponseDto
    {
        [JsonPropertyName("data")]
        public List<YGOProDeckCardDto> Data { get; set; } = new();
    }
    public class YGOProDeckCardDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("typeline")]
        public List<string> Typeeline { get; set; } = new();

        [JsonPropertyName("desc")]
        public string Description { get; set; } = "";

        [JsonPropertyName("race")]
        public string Race { get; set; } = "";

        [JsonPropertyName("atk")]
        public int? Atk { get; set; }

        [JsonPropertyName("def")]
        public int? Def { get; set; }

        [JsonPropertyName("level")]
        public int? Level { get; set; }

        [JsonPropertyName("attribute")]
        public string Attribute { get; set; } = "";

        [JsonPropertyName("archetype")]
        public string Archetype { get; set; } = "";
    }

    public class CardSetDto
    {
        public string SetName { get; set; } = "";

        public string SetCode { get; set; } = "";

        public string SetRarity { get; set; } = "";

        public string SetRarityCode { get; set; } = "";

        public string SetPrice { get; set; } = "";
    }
    public class BanlistInfoDto
    {
        public string? BanTcg { get; set; }

        public string? BanOcg { get; set; }

        public string? BanGoat { get; set; }
    }
    public class CardImageDto
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = "";

        public string ImageUrlSmall { get; set; } = "";

        public string ImageUrlCropped { get; set; } = "";
    }
    public class CardPriceDto
    {
        public string CardmarketPrice { get; set; } = "";

        public string TcgplayerPrice { get; set; } = "";

        public string EbayPrice { get; set; } = "";

        public string AmazonPrice { get; set; } = "";

        public string CoolstuffincPrice { get; set; } = "";
    }
}
