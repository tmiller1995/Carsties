using FluentValidation;

namespace Auction.Application.Auctions.Create;

public sealed class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(c => c.AuctionToCreate.Seller)
            .NotNull()
            .NotEmpty()
            .WithMessage("Seller is required.");

        RuleFor(c => c.AuctionToCreate.ReservePrice)
            .GreaterThanOrEqualTo(1m)
            .WithMessage("Reserve price must be greater than or equal to 1.");

        RuleFor(c => c.AuctionToCreate.AuctionEnd)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddDays(1))
            .WithMessage("Auction end must be greater than the current day.");

        RuleFor(c => c.AuctionToCreate.Item)
            .NotNull()
            .WithMessage("Item is required.");
    }
}