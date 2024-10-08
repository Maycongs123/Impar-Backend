﻿using Impar.Domain.Entities.Cards;
using Microsoft.AspNetCore.Http;
namespace Impar.DTO.Request.Cards
{
    /// <summary>
    /// Represents a card creation request.
    /// </summary>
    public class CreateCardRequest
    {
        /// <summary>
        /// The name of the card.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Photo of the card.
        /// </summary>
        public string Photo { get; set; }

        public Card ToDomain() => (Card)this;

        public static implicit operator Card(CreateCardRequest dto)
        {
            if (dto is null)
                return null;

            return new Card
            {
                Name = dto.Name,
                Status = CardStatusEnum.Active,
            };
        }
    }

}
