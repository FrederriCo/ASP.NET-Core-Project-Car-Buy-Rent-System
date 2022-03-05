namespace CarBuyRentSystem.Infrastructure.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 30;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;
            public const int YearMinValue = 1990;
            public const int YearMaxValue = 2030;
            public const int LugageMaxValue = 20;
            public const int DoorsMaxValue = 6;
            public const int PassagerMaxValue = 10;
            public const int ImageUrlMaxLength = 2083;

        }

        public class Location
        {
            public const int NameMaxLength = 30;
        }
    }
}
