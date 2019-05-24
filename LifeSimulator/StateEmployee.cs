
namespace LifeSimulator
{
    class StateEmployee : Student, IActivity
    {
        public string[] RestVarients => new string[]
        {
            $"Купить недвижимость({Salary * 10})",
            "Взять взятку",
            "Употребить"
        };

        public string[] WorkVarients => new string[]
        {
            "Дать взятку",
            "Сделать вид, что работаешь",
            "Отпроситься",
            "Взять взятку"
        };

        public StateEmployee(Student student) : base(student)
        {
            Salary = Charizma / 20m;
        }

        public StateEmployee(StateEmployee stateEmployee) :
            base(new Student(new Human(stateEmployee.Name, stateEmployee.Age,
                    stateEmployee.GetSex == "мужской" ? 0 : 1, stateEmployee.Happiness),
                stateEmployee.Intelligence, stateEmployee.Endurance, stateEmployee.Charizma))
        {
            Salary = Charizma / 20m;
        }

        public void Work()
        {
            Capital -= Salary * 5 - Charizma * 0.1m;
            Happiness -= 10;
            Endurance -= 10;
            RestDayCount = 0;
        }

        private void FireEmployee()
        {
            if (++RestDayCount == 10)
                throw new EndGameException("Вас уволили, вы бездельник!");
        }

        public void SpendTimeAtWork()
        {
            Capital += Salary;
            Endurance -= 10;
            Happiness -= 5;
            FireEmployee();
        }

        public void GoHome()
        {
            Happiness += 10;
            Endurance += 10;
            FireEmployee();
        }

        public void Relax()
        {
            Capital -= Salary * 10;
            Happiness = 100;
            Charizma++;
            Endurance = 80;
        }

        public void Drink()
        {
            Endurance = 40;
            Happiness = 70;
            Capital -= Salary * 3;
        }

        public void TakeActiveHoliday() => GetPresent();

        public void GetPresent()
        {
            Capital += Charizma * 0.1m + Salary;
            Happiness -= 5;
            Endurance -= 10;
            FireEmployee();
        }
    }
}
