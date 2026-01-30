using ConsistencyCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Import
{
    public class CardResolver
    {
        private readonly YgoProDeckClient _client = new();

        public async Task<Card> ResolveAsync(string id)
        {
            var dto = await _client.GetCardByIdAsync(id);

            if (dto == null)
            {
                return new Card
                {
                    Name = $"ID:{id}",
                    Tags = new HashSet<string>()
                };
            }

            var tags = new HashSet<string>
            {
                dto.Race.ToLower()
            };

            if (!string.IsNullOrWhiteSpace(dto.Archetype))
                tags.Add(dto.Archetype.ToLower());

            return new Card
            {
                Name = dto.Name,
                Tags = tags
            };
        }
    }
}
