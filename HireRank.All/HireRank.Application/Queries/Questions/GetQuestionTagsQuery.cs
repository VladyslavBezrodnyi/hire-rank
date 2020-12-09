using MediatR;
using System.Collections.Generic;

namespace HireRank.Application.Queries.Questions
{
    public class GetQuestionTagsQuery : IRequest<List<string>>
    {
    }
}
