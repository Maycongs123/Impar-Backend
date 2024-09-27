
using Dapper;
using Impar.Common.Configurations;
using Impar.Domain.Entities.Cards;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Impar.Repositories.Implementation
{
    internal class CardRepository : ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings.DefaultConnection;
        }
        public async Task Create(Card card)
        {
            var query = @"INSERT 
                        INTO 
                        Cards 
                        (Name, 
                        PhotoId, 
                        Status)
                        VALUES 
                        (@Name, 
                        @PhotoId, 
                        @Status);"; 
            
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteScalarAsync<int>(query, new
                {
                    Name = card.Name,
                    PhotoId = card.PhotoId,
                    Status = card.Status
                });
            }
        }
    }
}
