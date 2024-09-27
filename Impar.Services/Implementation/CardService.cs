using Impar.Common.DTO;
using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;
using Impar.Repositories;

namespace Impar.Services.Implementation
{
    internal class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository) 
        {
            _cardRepository = cardRepository;
        } 
        
        public async Task<Result<CardResponse>> Create(CardRequest request)
        {
            var card = request.ToDomain();

            await _cardRepository.Create(card);

            return Result.Success(card.ToResponse());
        }
    }
}
