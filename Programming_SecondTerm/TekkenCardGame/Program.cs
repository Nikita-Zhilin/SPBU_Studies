using System;

namespace TekkenCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в пошаговую игру по Tekken! Вам предстоит выбрать класс, персонажа, и сразиться с оппонентом!\n");
            Console.WriteLine("Но, для начала, выберете тип игры, введя число от 1 до 3:\n 1) Игрок против ИИ.\n 2) Игрок против игрока.\n 3) ИИ против ИИ.\n");
            int gameType = Convert.ToInt32 (Console.ReadLine());
            switch (gameType)
            {
                case 1:                                             // Запуск Игрок против ИИ
                    FighterSelection.printClassDescription();
                    Game.FightPvE();
                    break;
                case 2:                                             // Запуск Игрок против Игрока
                    FighterSelection.printClassDescription();
                    Game.FightPvP();
                    break;
                case 3:                                             // Запуск ИИ против ИИ
                    FighterSelection.printClassDescription();
                    Game.FightEvE();
                    break;
            }
        }
    }
}
