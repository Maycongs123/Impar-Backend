using Impar.Common.DTO;
using Impar.DTO.Request.Cards;
using Impar.DTO.Response.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impar.Services
{
    public interface ICardService
    {
        Task<Result<CardResponse>> Create(CardRequest request);
    }
}
