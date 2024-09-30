using Dapper;
using Impar.Common.Configurations;
using Impar.Common.Pagging;
using Impar.Domain.Entities.Cards;
using Impar.Domain.Filters.Cards;
using Impar.Domain.Views.Cards;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Text;

namespace Impar.Repositories.Implementation
{
    internal class CardRepository : ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings.DefaultConnection;
        }

        public async Task<int> Create(Card card)
        {
            var query = @"INSERT INTO Cards 
                  (Name, PhotoId, Status)
                  OUTPUT INSERTED.Id
                  VALUES 
                  (@Name, @PhotoId, @Status);";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var cardId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    Name = card.Name,
                    PhotoId = card.PhotoId,
                    Status = card.Status
                });

                return cardId;
            }
        }


        public async Task<int> CreatePhoto(string base64)
        {
            var query = @"INSERT INTO Photos (Base64)
                  OUTPUT INSERTED.Id
                  VALUES (@Base64);";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var photoId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    Base64 = base64
                });

                return photoId;
            }
        }

        public async Task<PageResponse<GetAllCardsView>> Get(GetAllCardsFilter filter)
        {
            var cardQueryBuilder = new StringBuilder(@"
                                              SELECT 
                                                  c.Id,
                                                  c.Name,
                                                  c.Status,
                                                  p.Id As PhotoId,
                                                  p.Base64 AS PhotoBase64
                                              FROM Cards c
                                              LEFT JOIN Photos p ON c.PhotoId = p.Id
                                              WHERE 1=1");

            var countQueryBuilder = new StringBuilder(@"
                                               SELECT COUNT(*)
                                               FROM Cards c
                                               LEFT JOIN Photos p ON c.PhotoId = p.Id
                                               WHERE 1=1");

            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                conditions.Add("LOWER(c.Name) LIKE LOWER(@Name + '%')");
                parameters.Add("Name", filter.Name); 
            }

            if (conditions.Any())
            {
                var conditionString = string.Join(" AND ", conditions);
                cardQueryBuilder.Append(" AND " + conditionString);
                countQueryBuilder.Append(" AND " + conditionString);
            }

            cardQueryBuilder.Append(@"
                            ORDER BY " + filter.OrderBy.Column + " " + filter.OrderBy.Direction + @"
                            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            parameters.Add("PageSize", filter.PageSize);
            parameters.Add("Offset", filter.Offset);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var cards = await connection.QueryAsync<GetAllCardsView>(
                    cardQueryBuilder.ToString(),
                    parameters
                );

                var totalItems = await connection.ExecuteScalarAsync<int>(
                    countQueryBuilder.ToString(),
                    parameters
                );

                return new PageResponse<GetAllCardsView>
                {
                    Items = cards.ToList(),
                    CurrentPage = filter.CurrentPage,
                    PageSize = filter.PageSize,
                    TotalItems = totalItems
                };
            }
        }

        public async Task<CardView> GetCardWithPhotoById(int id)
        {
            var query = @"
                       SELECT 
                           c.Id,
                           c.Name,
                           c.Status,
                           p.Id AS PhotoId,
                           p.Base64 AS PhotoBase64
                       FROM Cards c
                       LEFT JOIN Photos p ON c.PhotoId = p.Id
                       WHERE c.Id = @Id;";

            var param = new { Id = id };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<CardView>(query, param);
                return result;
            }
        }

        public async Task<Card> GetById(int id)
        {
            var query = @"
                       SELECT 
                           c.Id,
                           c.Name,
                           c.Status
                       FROM Cards c
                       WHERE c.Id = @Id;";

            var param = new { Id = id };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<Card>(query, param);
                return result;
            }
        }

        public async Task Delete(int id)
        {

            var getPhotoIdQuery = "SELECT PhotoId FROM Cards WHERE Id = @Id;";

            var deleteCardQuery = "DELETE FROM Cards WHERE Id = @Id;";

            var deletePhotoQuery = "DELETE FROM Photos WHERE Id = @PhotoId;";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var photoId = await connection.ExecuteScalarAsync<int?>(getPhotoIdQuery, new { Id = id });

                await connection.ExecuteAsync(deleteCardQuery, new { Id = id });

                if (photoId.HasValue)
                {
                    await connection.ExecuteAsync(deletePhotoQuery, new { PhotoId = photoId.Value });
                }
            }
        }


        public async Task Update(Card card)
        {
            var query = @"
                        UPDATE Cards 
                        SET 
                            Name = @Name, 
                            Status = @Status
                        WHERE Id = @Id;";

            var param = new
            {
                Id = card.Id,
                Name = card.Name,
                Status = card.Status
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task UpdatePhotoBase64(int photoId, string base64)
        {
            var query = @"
                 UPDATE Photos 
                 SET 
                     Base64 = @Base64
                 WHERE Id = @Id;";

            var param = new
            {
                Id = photoId,
                Base64 = base64
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, param);
            }
        }

    }
}
