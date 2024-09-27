using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;
using Impar.Services;
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

        [HttpGet(Name = "Cards")]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }

        [HttpPost("", Name = nameof(CreateCard))]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new card", Description = "Creates a new card and returns it")]
        [SwaggerResponse(200, "Created card", typeof(CardResponse))]
        public async Task<ActionResult> CreateCard([FromForm] CardRequest request)
        {
            //TODO: Validate Access

            return await _cardService.Create(request);
        }
    }
}
