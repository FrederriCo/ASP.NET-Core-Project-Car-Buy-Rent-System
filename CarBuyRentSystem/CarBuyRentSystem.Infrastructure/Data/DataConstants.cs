namespace CarBuyRentSystem.Infrastructure.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 30;

            public const int ModelMinLength = 1;
            public const int ModelMaxLength = 30;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;

            public const int YearMinValue = 1990;
            public const int YearMaxValue = 2030;

            public const int LugageMinValue = 1;
            public const int LugageMaxValue = 20;

            public const int DoorsMinValue = 1;
            public const int DoorsMaxValue = 6;

            public const int PassagerMinValue = 1;
            public const int PassagerMaxValue = 10;

            public const int ImageUrlMaxLength = 2083;

            public const string DecimalDefaultValue = "decimal(18,2)";
        }

        public class Location
        {
            public const int NameMaxLength = 30;
        }

        public class CarUser
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 30;
            public const int AddressMinLength = 3;
            public const int AddressMaxLength = 30;
            public const string DecimalDefaultValue = "decimal(18,2)";
        }

        public class Dealer
        {
            public const int NameDealerMinLength = 2;
            public const int NameDealerMaxLength = 20;
            public const int PhoneNumberMinLength = 4;
            public const int PhoneNumberMaxLength = 20;
        }
    }
}
