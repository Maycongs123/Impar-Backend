using Impar.Domain.Entities.Cards;

namespace Impar.Repositories
{
    public interface ICardRepository
    {
        Task Create(Card card);
    }
}
