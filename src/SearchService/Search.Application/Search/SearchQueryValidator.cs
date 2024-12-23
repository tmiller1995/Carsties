using FluentValidation;

namespace Search.Application.Search;

public sealed class SearchQueryValidator : AbstractValidator<SearchQuery>
{
    private static readonly HashSet<string> ValidOrderByOptions = ["make", "new"];
    private static readonly HashSet<string> ValidFilterByOptions = ["finished", "endingsoon"];

    public SearchQueryValidator()
    {
        RuleFor(sq => sq.AuctionSearch.OrderBy)
            .Custom((s, context) =>
            {
                if (!string.IsNullOrWhiteSpace(s) && !ValidOrderByOptions.Contains(s.ToLower()))
                    context.AddFailure($"{s} is not a valid order by option. Please specify one of the following: {string.Join(", ", ValidOrderByOptions)}");
            });

        RuleFor(sq => sq.AuctionSearch.FilterBy)
            .Custom((s, context) =>
            {
                if (!string.IsNullOrWhiteSpace(s) && !ValidFilterByOptions.Contains(s.ToLower()))
                    context.AddFailure($"{s} is not a valid filter by option. Please specify one of the following: {string.Join(", ", ValidFilterByOptions)}");
            });
    }
}