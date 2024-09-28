using Impar.Common.DTO;
using Impar.Common.Pagging;
using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;

namespace Impar.Services
{
    public interface ICardService
    {
        Task<Result<CardResponse>> Create(CreateCardRequest request);
        Task<Result<CardResponse>> GetById(int id);
        Task<Result<PageResponse<GetAllCardsResponse>>> Get(GetAllCardRequest request);
        Task<Result<CardResponse>> Delete(int id);
        Task<Result<CardResponse>> Patch(int id, PatchRequest request);

    }
}
