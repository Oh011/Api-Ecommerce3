namespace Shared.Dtos
{
    public record BasketDto
    {

        public string id { get; init; }

        public IEnumerable<BasketItemDto> Items { get; init; }
    }
}


//Using init instead of set makes it immutable after object creation.