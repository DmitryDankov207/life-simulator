using System;

namespace LifeSimulator
{
    class Workman : Student, IActivity
    {
        public string[] RestVarients => new string[]
        {
            "Посмотреть новости",
            "Спорить о политике",
            "Употребить нормально так"
        };

        public string[] WorkVarients => new string[]
        {
            "Поработать",
            "Сделать вид, что работаешь",
            "Отпроситься"
        };

        public Workman(Student student) : base(student)
        {
            Salary = Endurance / 20m;
        }

        public Workman(Workman workman) :
            base(new Student(new Human(workman.Name, workman.Age,
                    workman.GetSex == "мужской" ? 0 : 1, workman.Happiness),
                workman.Intelligence, workman.Endurance, workman.Charizma))
        {
            Salary = Endurance / 20m;
        }

        public void Work()
        {
            Happiness -= 5;
            Endurance -= 15;
            Capital += Salary;
            if (++WorkDayCount == 3)
                Salary += Endurance * 0.01m;
        }

        public void SpendTimeAtWork()
        {
            Happiness -= 5;
            Endurance -= 5;
            Capital += Salary;
            WorkDayCount = 0;
        }

        public void GoHome()
        {
            Happiness += 5;
            Endurance += 10;
            WorkDayCount = 0;
        }

        public void Relax()
        {
            Endurance = 90;
            Capital -= Salary / 2;
            WorkDayCount = 0;
        }

        public void Drink()
        {
            Endurance = 40;
            Happiness = 100;
            Capital -= Salary;
            WorkDayCount = 0;
        }

        public void TakeActiveHoliday()
        {
            if(new Random().Next(2) == 1)
                Drink();
            Endurance += 30;
            Happiness -= 30;
            Capital -= Salary / 4;
            WorkDayCount = 0;
        }

        public void GetPresent()
        {
            throw new NotImplementedException();
        }
    }
}
