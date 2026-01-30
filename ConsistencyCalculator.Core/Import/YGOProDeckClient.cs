using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Core.Import
{
    public class YgoProDeckClient
    {
        private readonly HttpClient _http = new();

        public async Task<YGOProDeckCardDto?> GetCardByIdAsync(string id)
        {
            var url = $"https://db.ygoprodeck.com/api/v7/cardinfo.php?id={id}";

            var json = await _http.GetStringAsync(url); 
            
            var response = JsonSerializer.Deserialize<YGOProDeckResponseDto>(json);

            if (response == null)
                throw new Exception("Carta não encontrada na YGOProDeck API");
            

            return response?.Data.First();
        }
    }
}
