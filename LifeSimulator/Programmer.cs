using System;

namespace LifeSimulator
{
    public delegate void SuccessEvent(string message);

    class  Programmer : Student, IActivity
    {
        public static SuccessEvent CompleteProject { get; set; }

        public string[] RestVarients => new string[]
        {
            "Почитать проф. литературу",
            "Отдохнуть по-хипстерски",
            "Употребить немного"
        };

        public string[] WorkVarients => new string[]
        {
            "Поработать",
            "Сделать вид, что работаешь",
            "Отпроситься"
        };

        public Programmer(Student student) : base(student)
        {
            Salary = Intelligence / 10m;
        }

        public Programmer(Programmer programmer) :
            base(new Student(new Human(programmer.Name, programmer.Age,
                    programmer.GetSex == "мужской" ? 0 : 1, programmer.Happiness),
                programmer.Intelligence, programmer.Endurance, programmer.Charizma))
        {
            Salary = Intelligence / 10m;
        }

        public void Work()
        {
            Capital += Salary;
            Happiness -= 5;
            Endurance -= 10;
            if (++WorkDayCount == 10)
            {
                WorkDayCount = 0;
                if (CompleteProject != null)
                    CompleteProject("\nПоздравляем! вы сдали проект!");
                Salary = (Intelligence += Intelligence / 20) / 10;
                Capital += Salary * 5;
            }
        }

        public void SpendTimeAtWork()
        {
            Capital += Salary;
            Happiness -= 5;
            if(++RestDayCount == 10)
                throw new EndGameException("\nВас уволили, вы бездельник!");
        }

        public void GoHome()
        {
            WorkDayCount = 0;
            Happiness += 10;
            Endurance += 10;
            if (++RestDayCount == 5)
                throw new EndGameException("\nВас уволили, вы бездельник!");
        }

        void ResetRestDays()
        {
            if (WorkDayCount == 5)
                RestDayCount = 0;
        }

        public void Relax()
        {
            Endurance += 30;
            Capital -= 0.5m * Salary;
            ResetRestDays();
        }

        public void Drink()
        {
            Endurance = 40;
            Happiness = 90;
            Capital -= Salary;
            ResetRestDays();
        }

        public void TakeActiveHoliday()
        {
            Endurance -= 10;
            Happiness += 25;
            Capital -= 2m * Salary;
            ResetRestDays();
        }

        public void GetPresent()
        {
            throw new NotImplementedException();
        }
    }
}
