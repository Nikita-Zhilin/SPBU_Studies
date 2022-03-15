using System;
using System.Collections.Generic;
using System.Text;

namespace TekkenCardGame
{
    public class FighterSelection
    {
        public static void printClassDescription()
        {
            Console.WriteLine("Пришло время выбрать класс вашего персонажа! Введите число от 1 до 3\n" +
                "1) Тяжелые бойцы -способны блокировать часть получаемого урона.\n" +
                "2) Бойцы, владеющие боевыми искусствами - обладают способностью увеличивать урон от собственных атак.\n" +
                "3) Маги - могут использовать альтернативные магические атаки (нельзя использовать подряд).\n");
        }

        public static Characters ClassFighterChoise(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Тяжелые бойцы, введя соответствующий номер:\n");
                    FighterSelection.printHeavyFightersDescription();
                    int fighter = Convert.ToInt32(Console.ReadLine());
                    return FighterSelection.ChooseHeavyFighters(fighter);

                case 2:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Бойцы, владеющие боевыми искусствами, введя соответствующий номер:\n");
                    FighterSelection.printMartialArtsDescription();
                    fighter = Convert.ToInt32(Console.ReadLine());
                    return FighterSelection.ChooseMartialArts(fighter);

                case 3:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Маги, введя соответствующий номер:\n");
                    FighterSelection.printMagesDescription();
                    fighter = Convert.ToInt32(Console.ReadLine());
                    return FighterSelection.ChooseMages(fighter);
            }
            return FighterSelection.ChooseHeavyFighters(1);
        }

        public static Characters ClassFighterChoiseAI(int choice)
        {
            Random random = new Random();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Тяжелые бойцы, введя соответствующий номер:\n");
                    FighterSelection.printHeavyFightersDescription();
                    int fighter = random.Next(1, 5);
                    return FighterSelection.ChooseHeavyFighters(fighter);

                case 2:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Бойцы, владеющие боевыми искусствами, введя соответствующий номер:\n");
                    FighterSelection.printMartialArtsDescription();
                    fighter = random.Next(1, 5);
                    return FighterSelection.ChooseMartialArts(fighter);

                case 3:
                    Console.WriteLine("\nИгрок 1, выберете бойца класса Маги, введя соответствующий номер:\n");
                    FighterSelection.printMagesDescription();
                    fighter = random.Next(1, 5);
                    return FighterSelection.ChooseMages(fighter);
            }
            return FighterSelection.ChooseHeavyFighters(1);
        }


        public static void printHeavyFightersDescription()
        {
            Console.Write("1. ");
            Jack.description();
            Console.Write("2. ");
            Panda.description();
            Console.Write("3. ");
            Gigas.description();
            Console.Write("4. ");
            Bob.description();
            Console.Write("5. ");
            Kuma.description();
        }

        public static void printMartialArtsDescription()
        {
            Console.Write("1. ");
            Heihachi.description();
            Console.Write("2. ");
            Kazuya.description();
            Console.Write("3. ");
            King.description();
            Console.Write("4. ");
            Bryan.description();
            Console.Write("5. ");
            Dragunov.description();
        }

        public static void printMagesDescription()
        {
            Console.Write("1. ");
            Zafina.description();
            Console.Write("2. ");
            DevilJin.description();
            Console.Write("3. ");
            Kazumi.description();
            Console.Write("4. ");
            Eliza.description();
            Console.Write("5. ");
            Akuma.description();
        }

        public static HeavyFighters ChooseHeavyFighters(int n)
        {
            switch (n)
            {
                case 1:
                    return new Jack();
                case 2:
                    return new Panda();
                case 3:
                    return new Gigas();
                case 4:
                    return new Bob();
                case 5:
                    return new Kuma();
            }
            Console.Write("Введено некоректное значение. Выбор будет сделан за вас...");
            return new Jack();
        }

        public static MartialArts ChooseMartialArts(int n)
        {
            switch (n)
            {
                case 1:
                    return new Heihachi();
                case 2:
                    return new Kazuya();
                case 3:
                    return new King();
                case 4:
                    return new Bryan();
                case 5:
                    return new Dragunov();
            }
            Console.Write("Введено некоректное значение. Выбор будет сделан за вас...");
            return new Heihachi();
        }

        public static Mages ChooseMages(int n)
        {
            switch (n)
            {
                case 1:
                    return new Zafina();
                case 2:
                    return new DevilJin();
                case 3:
                    return new Kazumi();
                case 4:
                    return new Eliza();
                case 5:
                    return new Akuma();
            }
            Console.Write("Введено некоректное значение. Выбор будет сделан за вас...");
            return new Akuma();
        }
    }
}
