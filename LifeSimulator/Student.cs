using System;

namespace LifeSimulator
{
    public delegate void HeroStateHandler<U>(string message)
        where U : unmanaged;

    public class Student : Human, IActivity
    {
        public int WorkDayCount { get; protected set; } = 0;

        public int RestDayCount { get; protected set; } = 0;
        public static string Greeting => "\nСтуденческие годы - весна юности!";

        private decimal _intelligence;

        private static decimal SetStatus(ref decimal state, decimal value) 
            => state = value < 0 ? 0 : value > 100 ? 100 : value;


        public decimal Intelligence
        {
            get => _intelligence;
            set
            {
                SetStatus(ref _intelligence, value);
                SetAttributeEvent($"Интеллект: {_intelligence}");
            }
        }

        private decimal _endurance;

        public event HeroStateHandler<decimal> DecimalAttributeChanged;

        public string[] RestVarients { get; }

        public string[] WorkVarients { get; }

        public decimal Endurance
        {
            get => _endurance;
            set
            {
                SetStatus(ref _endurance, value);
                SetAttributeEvent($"Выносливость: {_endurance}");
                if (_endurance < 0)
                    throw new EndGameException("\nВаш организм уже давно работает " +
                                               "на износ,\nвы падаете без пульса.");
            }
        }

        private decimal _charizma;

        public decimal Charizma
        {
            get => _charizma;
            set
            {
                SetStatus(ref _charizma, value);
                SetAttributeEvent($"Харизма: {_charizma}");
                if (_endurance < 0)
                    throw new EndGameException("Вы настолько на втором плане, " +
                                               "\nчто когда вас сбила машина, никто не заметил");
            }
        }

        private decimal _salary;

        public decimal Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                SetAttributeEvent($"Текущий оклад: {_salary}");
            }
        }

        void SetAttributeEvent(string message)
        {
            if (DecimalAttributeChanged != null)
                DecimalAttributeChanged(message);
        }

        public void Work()
        {
            throw new NotImplementedException();
        }

        public void GetPresent()
        {
            throw new NotImplementedException();
        }

        public void SpendTimeAtWork()
        {
            throw new NotImplementedException();
        }

        public void GoHome()
        {
            throw new NotImplementedException();
        }

        public void Relax()
        {
            throw new NotImplementedException();
        }

        public void Drink()
        {
            throw new NotImplementedException();
        }

        public void TakeActiveHoliday()
        {
            throw new NotImplementedException();
        }

        private decimal _capital;

        public decimal Capital
        {
            get => _capital;
            set
            {
                _capital = value;
                SetAttributeEvent($"Капитал: {_capital}");
                if (_capital < 0)
                    throw new EndGameException("Вам не на что жить!");
            }
        }

        public Student(Human human, decimal intelligence = 50m, decimal endurance = 50,
            decimal charizma = 50) : base(human)
        {
            _intelligence = intelligence;
            _endurance = endurance;
            _charizma = charizma;
        }

        public Student(Student student) : base(human: new Human(name:student.Name,
            age:student.Age, student.GetSex == "мужской" ? 0 : 1, student.Happiness))
        {
            _intelligence = student.Intelligence;
            _endurance = student.Endurance;
            _charizma = student.Charizma;
        }

    }

    class EndGameException : Exception
    {
        private string _reason;
        public EndGameException(string reason = "") : base()
        {
            _reason = reason;
        }
        public override string Message => $"\n{_reason}\nИгра окончена!";
    }
}
