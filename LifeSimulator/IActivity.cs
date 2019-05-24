
namespace LifeSimulator
{
    public interface IActivity
    {
        event HeroStateHandler<decimal> DecimalAttributeChanged;

        event HeroStateHandler<int> IntAttributeChanged;

        string[] RestVarients { get; }

        string[] WorkVarients { get; }

        int Happiness { get; set; }

        decimal Endurance { get; set; }

        decimal Intelligence { get; set; }

        decimal Charizma { get; set; }

        string GetSex { get; }

        int Age { get; set; }

        decimal Salary { get; set; }

        decimal Capital { get; set; }

        string Name { get; set; }

        void Work();

        void GetPresent();

        void SpendTimeAtWork();

        void GoHome();

        void Relax();

        void Drink();

        void TakeActiveHoliday();
    }
}
