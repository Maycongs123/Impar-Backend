using Impar.Common.DTO;
using Impar.Common.Pagging;
using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;
using Impar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Impar.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("", Name = nameof(CreateCard))]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new card", Description = "Creates a new card and returns it")]
        [SwaggerResponse(200, "Created card", typeof(CardResponse))]
        public async Task<ActionResult> CreateCard([FromForm] CreateCardRequest request)
        {
            return await _cardService.Create(request);
        }

        [HttpGet("{id:int}", Name = nameof(GetById))]
        [SwaggerOperation(Summary = "Get card by Id", Description = "Card details by ID")]
        [SwaggerResponse(200, "Card found", typeof(CardResponse))]
        public async Task<ActionResult> GetById(
            [FromRoute, SwaggerParameter("ID of the card", Required = true)] int id)
        {

            return await _cardService.GetById(id);
        }

        [HttpGet("", Name = nameof(GetAllCards))]
        [SwaggerOperation(Summary = "Get all cards", Description = "Get all cards with pagination")]
        [SwaggerResponse(200, "Cards found", typeof(PageResponse<GetAllCardsResponse>))]
        public async Task<ActionResult> GetAllCards([FromQuery] GetAllCardRequest request)
        {
            return await _cardService.Get(request);
        }

        [HttpPatch("{id}", Name = nameof(PatchCard))]
        [SwaggerOperation(Summary = "Edit card", Description = "Edit card, only changing values that are not null")]
        [SwaggerResponse(200, "Edited card", typeof(CardResponse))]
        public async Task<ActionResult> PatchCard(int id, [FromBody] PatchRequest request)
        {
            return await _cardService.Patch(id, request);
        }

        [HttpDelete("{id:int}", Name = nameof(DeleteCard))]
        [SwaggerOperation(Summary = "Delete card by Id", Description = "Delete card by ID")]
        [SwaggerResponse(200, "card deleted", typeof(Result))]
        public async Task<ActionResult> DeleteCard(
           [FromRoute, SwaggerParameter("ID of the card", Required = true)]
            int id)
        {
            return await _cardService.Delete(id);
        }
    }
}
