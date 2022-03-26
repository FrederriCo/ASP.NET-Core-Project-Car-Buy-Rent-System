namespace CarBuyRentSystem.Core.Models.Cars
{
    public interface ICarModel
    {
        int Year { get; }
        decimal Price { get; }
        string Model { get; }

    }
}
