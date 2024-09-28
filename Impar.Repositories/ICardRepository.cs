using Impar.Common.Pagging;
using Impar.Domain.Entities.Cards;
using Impar.Domain.Entities.Photos;
using Impar.Domain.Filters.Cards;
using Impar.Domain.Views.Cards;

namespace Impar.Repositories
{
    public interface ICardRepository
    {
        Task<int> Create(Card card);
        Task<PageResponse<GetAllCardsView>> Get(GetAllCardsFilter filter);
        Task<int> CreatePhoto(string base64);
        Task UpdatePhotoBase64(int photoId, string base64);
        Task<Card> GetById(int id);
        Task<CardView> GetCardWithPhotoById(int id);
        Task Delete(int id);
        Task Update(Card card);
    }
}
