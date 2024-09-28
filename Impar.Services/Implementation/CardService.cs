using Impar.Common.Constants;
using Impar.Common.DTO;
using Impar.Common.Pagging;
using Impar.Domain.Entities.Cards;
using Impar.Domain.Views.Cards;
using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;
using Impar.Repositories;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Impar.Services.Implementation
{
    internal class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository) 
        {
            _cardRepository = cardRepository;
        }

        public async Task<Result<CardResponse>> Create(CreateCardRequest request)
        {
            string base64Photo = await ConvertToBase64Async(request.Photo);

            int photoId = await _cardRepository.CreatePhoto(base64Photo);

            Card card = request.ToDomain();

            card.PhotoId = photoId;

            int cardId = await _cardRepository.Create(card);

            card.Id = cardId; 

            return Result.Success(card.ToResponse(base64Photo));
        }


        public async Task<string> ConvertToBase64Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }


        public async Task<Result<CardResponse>> GetById(int id)
        {
            CardView card = await _cardRepository.GetCardWithPhotoById(id);
            if (card is null)
            {
                return Result.Failure(Messages.CardNotFound, HttpStatusCode.NotFound);
            }

            return Result.Success(card.ToResponse());
        }

        public async Task<Result<PageResponse<GetAllCardsResponse>>> Get(GetAllCardRequest request)
        {
            var cardsPage = await _cardRepository.Get(request.ToPageFilter());

            var response = cardsPage.ConvertItems(card => card.ToResponse());
            return Result.Success(response);
        }

        public async Task<Result<CardResponse>> Delete(int id)
        {
            await _cardRepository.Delete(id);

            return Result.Success();
        }

        public async Task<Result<CardResponse>> Patch(int id, PatchRequest request)
        {
            CardView card = await _cardRepository.GetCardWithPhotoById(id);
            if (card is null)
            {
                return Result.Failure(Messages.CardNotFound, HttpStatusCode.NotFound);
            }

            card.Name = request.Name ?? card.Name;
            card.Status = request.Status ?? card.Status;

            if (request.PhotoId.HasValue)
            {
                await _cardRepository.UpdatePhotoBase64((int)request.PhotoId, request.PhotoBase64);
            }

            await _cardRepository.Update(card);

            return Result.Success(card.ToResponse());
        }
    }
}
