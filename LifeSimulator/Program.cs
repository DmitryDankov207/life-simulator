using System;
using System.Collections.Generic;
using static System.Console;
using System.Media;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace LifeSimulator
{
    class Program
    {


        public static int GetInt()
        {
            while (true)
            {
                if (int.TryParse(ReadLine(), out int value))
                    return value;

                WriteLine("\nОшибка ввода!");
            }
        }

        static Dictionary<string, string[]> QuastionDictionary => new Dictionary<string, string[]>
        {
            {
                " " , new string[]
                {
                    "1 - Верно, не стоит тратить ее на учебу, лучше как следует развлечься.",
                    "2 - Лучше найду вышку по-проще и параллельно пойду работать",
                    "3 - Мне следует больше учиться."
                }
            },
            {
                "Какой подход к учебе вам более близок?" , new string[]
                {
                    "1 - Сочетаю хорошее зрение с искусством оставаться незамеченным",
                    "2 - Как нормальный тип, 50/50 учу/катаю",
                    "3 - Конечно учу, мне это позже пригодится"
                }
            },
            {
                "У вас начинаются проблемы с учебой" , new string[]
                {
                    "1 - Да пофиг, разберёмся",
                    "2 - Время бросить универ",
                    "3 - Поднапрячься и вывезти"
                }
            },
            {
                "Поздравляем! вы выпускник!" , new string[]
                {
                    "1 - Напьюсь так, чтобы ползать",
                    "2 - Я уже давно отчислился, так что пофиг",
                    "3 - Культурно посидеть c приятелями из универа"
                }
            },
        };

        static void Choose(ref (int, int, int) attributes)
        {
            while (true)
            {
                switch (GetInt())
                {
                    case 1:
                        attributes.Item1 += 10;
                        attributes.Item2 -= 5;
                        attributes.Item3 -= 5;
                        return;
                    case 2:
                        attributes.Item1 -= 5;
                        attributes.Item2 += 10;
                        attributes.Item3 -= 5;
                        return;
                    case 3:
                        attributes.Item1 -= 5;
                        attributes.Item2 -= 5;
                        attributes.Item3 += 10;
                        return;
                    default:
                        WriteLine("\nНеверный ввод!");
                        break;
                }
            }
        }

        static (int, int, int) GetAttributes()
        {
            (int, int, int) attributes = (50, 50, 50);

            foreach (var quastion in QuastionDictionary.Keys)
            {
                WriteLine(quastion);
                QuastionDictionary.TryGetValue(quastion, out string[] answers);
                foreach (var answer in answers)
                    WriteLine(answer);
                Choose(ref attributes);
            }

            return attributes;
        }

        static void GenerateHuman(out Human human)
        {
            while (true)
                try
                {
                    WriteLine("\nВведите имя персонажа: ");
                    string name = ReadLine();

                    WriteLine("\nВведите возраст: ");
                    int age = GetInt();

                    WriteLine("\nВыбeрите пол(0 - муж.; 1 - жен.): ");
                    int sex = GetInt();

                    human = new Human(name, age, sex);
                    return;
                }
                catch (Exception e)
                {
                    WriteLine(e);
                }
        }

        static void HaveWorkDay<U>(ref U hero)
            where U : IActivity
        {
            while (true)
            {
                WriteLine("\nЧем заняться на работе?");
                for (int i = 0; i < hero.WorkVarients.Length; i++)
                    WriteLine($"{i + 1} - {hero.WorkVarients[i]}");

                switch (GetInt())
                {
                    case 1:
                        hero.Work();
                        return;
                    case 2:
                        hero.SpendTimeAtWork();
                        return;
                    case 3:
                        hero.GoHome();
                        return;
                    case 4:
                        try
                        {
                            hero.GetPresent();
                            return;
                        }
                        catch (NotImplementedException e)
                        {
                            goto default;
                        }
                    default:
                        WriteLine("\nНеверный ввод!");
                        break;
                }
            }
        }

        static void HaveRestDay<U>(ref U hero)
            where U : IActivity
        {
            while (true)
            {
                WriteLine("\nКак желаете провести выходной?");
                for (int i = 0; i < hero.RestVarients.Length; i++)
                    WriteLine($"{i + 1} - {hero.RestVarients[i]}");

                switch (GetInt())
                {
                    case 1:
                        hero.Relax();
                        return;
                    case 2:
                        hero.TakeActiveHoliday();
                        return;
                    case 3:
                        hero.Drink();
                        return;
                    default:
                        WriteLine("\nНеверный ввод!");
                        break;
                }
            }
        }

        static void PrintHeroStats(in IActivity hero)
        {
            WriteLine($"\nИмя: {hero.Name}" + $"\nВозраст: {hero.Age}" +
                      $"\nПол: {hero.GetSex}" + $"\nХаризма: {hero.Charizma}" +
                      $"\nВыносливость: {hero.Endurance}" + $"\nИнтеллект: {hero.Intelligence}" +
                      $"\nКапитал: {hero.Capital}" + $"\nОклад: {hero.Salary}" +
                      $"\nУдовлетворенность: {hero.Happiness}");
        }

        static void PlayGame<U>(ref U hero) where U : IActivity
        {
            var spendedTime = 0;
            hero.DecimalAttributeChanged += (mes) => WriteLine(mes);
            hero.IntAttributeChanged += (mes) => WriteLine(mes);

            while (true)
            {
                spendedTime++;

                WriteLine("\n\tЧем сегодня заняться?\n1 - поработать" +
                          "\n2 - отдохнуть\n3 - Инфо\n4 - Посмотреть топ\n5 - надоело");
                try
                {
                    switch (int.Parse(ReadLine()))
                    {
                        case 1:
                            HaveWorkDay(ref hero);
                            break;
                        case 2:
                            HaveRestDay(ref hero);
                            break;
                        case 3:
                            PrintHeroStats(hero);
                            break;
                        case 4:
                            PrintLegends();
                            break;
                        case 5:
                            ChangeDb(hero, spendedTime);
                            return;
                    }
                }
                catch (EndGameException e)
                {
                    ChangeDb(hero, spendedTime);
                    WriteLine(e);
                    return;
                }
            }
        }

        static void ChangeDb<U>(in U hero, in int spendedTime) 
            where U : IActivity
        {
            using(PlayerContext db = new PlayerContext())
            {
                db.Players.Add(new Player
                {
                    Name = hero.Name,
                    Happiness = hero.Happiness,
                    SpendedTime = spendedTime
                });
                db.SaveChanges();
            }
        }

        static void PrintLegends()
        {
            using(PlayerContext db = new PlayerContext())
            {
                PrintImage();
                if (db.Players.ToArray().Length == 0)
                    WriteLine("\nЛегенд нет!");
                else
                {
                    string str = "";
                    for (int i = 0; i < 7; i++)
                        str += "\t";
                    WriteLine($"{str}Имя : Прожито(дней) : " +
                        $"Счастье");
                    var players = from p in db.Players.ToList()
                                  orderby p.SpendedTime descending
                                  select p;
                    foreach (var player in players.Take(players.ToArray()
                        .Length >= 10 ? 10 : players.ToArray().Length))
                        WriteLine($"{str}{player.Name} : {player.SpendedTime} : " +
                            $"{player.Happiness}");
                }
            }
        }

        static void PrintImage()
        {
            Image Picture = Image.FromFile(@"E:\Development\ISP\LifeSimulator\LifeSimulator\Images\" +
                @"legends.gif");
            Console.SetBufferSize((Picture.Width * 0x2), (Picture.Height * 0x2));
            FrameDimension Dimension = new FrameDimension(Picture.FrameDimensionsList[0x0]);
            int FrameCount = Picture.GetFrameCount(Dimension);
            int Left = Console.WindowLeft, Top = Console.WindowTop;
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            Picture.SelectActiveFrame(Dimension, 0x0);
            for (int i = 0x0; i < Picture.Height; i++)
            {
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color Color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    Console.Write(Chars[Index]);
                }
                Console.Write('\n');
            }
        }

        private enum Profession
        {
            Workman = 3,
            Programmer = 1,
            StateEmployee = 2
        }

        static Profession ChooseProfession(in (int, int, int) attributes)
        {
            string profession = attributes.Item1 > attributes.Item2 &&
                                attributes.Item1 > attributes.Item3 ? "гос. служащий" :
                                    attributes.Item2 > attributes.Item3 ?
                                        "рабочий" : "программист";
            WriteLine("\nВыберите профессию:\n1 - програмист(easy)" +
                      "\n2 - гос. служащий(medium)\n3 - рабочий(hardcore)" +
                      $"\nРекомендованно - {profession}");

            while (true)
            {
                if (int.TryParse(ReadLine(), out int value) &&
                    value >= 1 && value <= 3)
                    return (Profession)value;

                WriteLine("\nВвод выполнен неверно!");
            }
        }

        static void StartGame()
        {
            Console.WriteLine(Human.Greeting);
            Human human;
            GenerateHuman(out human);
            Console.WriteLine(Student.Greeting);
            Student student = new Student(human);
            student.Capital = 0;

            (int, int, int) attributes = GetAttributes();
            student.Charizma = attributes.Item1;
            student.Endurance = attributes.Item2;
            student.Intelligence = attributes.Item3;

            switch (ChooseProfession(in attributes))
            {
                case Profession.Workman:
                    PlayMusic(@"E:\Development\ISP\LifeSimulator\LifeSimulator\Music\Кино " +
                        @"- Пачка сигарет [Рифмы и Панчи].wav");
                    Workman workman = new Workman(student);
                    PlayGame(ref workman);
                    break;
                case Profession.Programmer:
                    PlayMusic(@"E:\Development\ISP\LifeSimulator\LifeSimulator\Music\" +
                        @"twenty one pilots - Heathens.wav");
                    Programmer programmer = new Programmer(student);
                    PlayGame(ref programmer);
                    break;
                case Profession.StateEmployee:
                    PlayMusic(@"E:\Development\ISP\LifeSimulator\LifeSimulator\Music\K. " +
                        @"Michelle feat. Gucci Mane - Self Made.wav");
                    StateEmployee stateEmployee = new StateEmployee(student);
                    PlayGame(ref stateEmployee);
                    break;
            }
        }

        static SoundPlayer BgSoundPlayer { get; set; }

        static async Task PlayMusic(string connectionString)
        {
            BgSoundPlayer = new SoundPlayer(connectionString);
            BgSoundPlayer.Stop();
            BgSoundPlayer.Play();
        }

        static void Main()
        {
            PlayMusic(@"E:\Development\ISP\LifeSimulator\LifeSimulator\Music\" +
                @"Bob Marley - Rastaman Chant.wav");
            StartGame();
            ReadKey();
        }
    }
}
