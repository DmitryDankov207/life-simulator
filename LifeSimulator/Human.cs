using System;
using System.Linq;

namespace LifeSimulator
{
    public class Human
    {
        public event HeroStateHandler<int> IntAttributeChanged;

        private int? _id;

        public int? Id
        {
            get
            {
                if (_id == null)
                    throw new Exception("\nУ этой персоны нет id!");
                else
                    return _id;
            }
            protected set
            {
                if (_id == null)
                    _id = value;
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (value.Where(c => char.IsDigit(c)).ToArray().Length != 0)
                    throw new Exception("\nОшибка ввода имени!");
                else
                    _name = value;
            }
        }

        public enum Sex
        {
            Man,
            Woman
        }

        private string _sex;

        public string GetSex => _sex;

        private int SetSex
        {
            set
            {
                if(_sex == null)
                    switch ((Sex)value)
                    {
                        case Sex.Man:
                            _sex = "мужской";
                            break;
                        case Sex.Woman:
                            _sex = "женский";
                            break;
                        default:
                            throw new PsychologyException();
                    }
            }
        }

        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                if (value < 0)
                    throw new Exception("\nОшибка ввода возраста!");
                else
                    _age = value;
            }
        }

        private int _happiness;

        public int Happiness
        {
            get => _happiness;
            set
            {
                _happiness = value < 0 ? 0 : value > 100 ? 100 : value;
                if (IntAttributeChanged != null)
                    IntAttributeChanged($"Уровень удволетворенности: {_happiness}");
                if(_happiness < 0)
                    throw new EndGameException("\nВы перестали видеть в жизни какой-либо смысл.\nВ приступе отчаяния вы прыгаете с крыши.");
            }
        }

        public static string Greeting => "\nВам повезло родиться человеком(но это не точно)!";

        public Human(string name, int age, int sex, int happiness = 50)
        {
            Name = name;
            Age = age;
            SetSex = sex;
            Happiness = happiness;
        }

        public Human(Human human)
        {
            Name = human.Name;
            Age = human.Age;
            _sex = human.GetSex;
            Happiness = human.Happiness;
        }
    }

    class PsychologyException : Exception
    {
        public override string Message => "\nТут вам не Европа!";
    }
}
