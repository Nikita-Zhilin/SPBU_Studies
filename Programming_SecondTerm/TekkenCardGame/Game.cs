using System;

namespace TekkenCardGame
{
    class Game
    {
        public static void FightPvP()       // ------------------------------Игрок против Игрока
        {
            FighterSelection selection = new FighterSelection();

            Console.WriteLine("\nИгрок 1, выберете свой класс:\n");
            int classPlayer1 = Convert.ToInt32(Console.ReadLine());

            var Player1 = FighterSelection.ClassFighterChoise(classPlayer1);

            Console.WriteLine("\nИгрок 1 выбрал персонажа " + Player1.name, "\n");

            Console.WriteLine("\nИгрок 2, выберете свой класс:\n");
            int classPlayer2 = Convert.ToInt32(Console.ReadLine());

            var Player2 = FighterSelection.ClassFighterChoise(classPlayer2);

            Console.WriteLine("\nИгрок 2 выбрал персонажа " + Player2.name, "\n");

            while ((Player1.health >= 0) || (Player2.health >= 0))
            {
                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 1:\n \n");
                int actionChoice = 1;

                if ((Player1.type == "Mages") && !(Player1.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player1.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = Convert.ToInt32(Console.ReadLine());
                }
                
                
                switch (actionChoice)
                {
                    case 1:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.getBlockedDamage(Player1.basicAttackDamage);
                        }
                        if (Player1.type == "MartialArts")
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                            if (Player1.checkBoost)
                            {
                                Player1.basicAttackDamage -= Player1.attackBoost;
                                Player1.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.abilityHeavyFighters();
                            Player1.uniqueAbility();
                        } 
                        if (Player1.type == "MartialArts")
                        {
                            Player1.getAttackBoost();
                            Player1.uniqueAbility();
                        }
                        else
                        {
                            if (Player2.type == "HeavyFighters")
                            {
                                Player2.getBlockedDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player2.getDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player2.health <= 0)
                {
                    Console.WriteLine(" \nИгра закончилась, победил Игрок 1 \n");
                    break;
                }

                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 2:\n  \n");

                if ((Player2.type == "Mages") && !(Player2.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player2.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = Convert.ToInt32(Console.ReadLine());
                }


                switch (actionChoice)
                {
                    case 1:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.getBlockedDamage(Player2.basicAttackDamage);
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                            if (Player2.checkBoost)
                            {
                                Player2.basicAttackDamage -= Player2.attackBoost;
                                Player2.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.abilityHeavyFighters();
                            Player2.uniqueAbility();
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player2.getAttackBoost();
                            Player2.uniqueAbility();
                        }
                        else
                        {
                            if (Player1.type == "HeavyFighters")
                            {
                                Player1.getBlockedDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player1.getDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player1.health <= 0)
                {
                    Console.WriteLine(" \n Игра закончилась, победил Игрок 2 \n");
                    break;
                }
            }
        }

        public static void FightPvE()       // ------------------------------Игрок против ИИ
        {
            FighterSelection selection = new FighterSelection();
            Random random = new Random();

            Console.WriteLine("\nИгрок 1, выберете свой класс:\n");
            int classPlayer1 = Convert.ToInt32(Console.ReadLine());

            var Player1 = FighterSelection.ClassFighterChoise(classPlayer1);

            Console.WriteLine("\nИгрок 1 выбрал персонажа " + Player1.name, "\n");

            Console.WriteLine("\nИгрок 2, выберете свой класс:\n");
            int classPlayer2 = random.Next(1, 3);

            var Player2 = FighterSelection.ClassFighterChoiseAI(classPlayer2);

            Console.WriteLine("\nИгрок 2 выбрал персонажа " + Player2.name, "\n");

            while ((Player1.health >= 0) || (Player2.health >= 0))
            {
                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 1:\n \n");
                int actionChoice = 1;

                if ((Player1.type == "Mages") && !(Player1.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player1.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = Convert.ToInt32(Console.ReadLine());
                }


                switch (actionChoice)
                {
                    case 1:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.getBlockedDamage(Player1.basicAttackDamage);
                        }
                        if (Player1.type == "MartialArts")
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                            if (Player1.checkBoost)
                            {
                                Player1.basicAttackDamage -= Player1.attackBoost;
                                Player1.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.abilityHeavyFighters();
                            Player1.uniqueAbility();
                        }
                        if (Player1.type == "MartialArts")
                        {
                            Player1.getAttackBoost();
                            Player1.uniqueAbility();
                        }
                        else
                        {
                            if (Player2.type == "HeavyFighters")
                            {
                                Player2.getBlockedDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player2.getDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player2.health <= 0)
                {
                    Console.WriteLine(" \nИгра закончилась, победил Игрок 1 \n");
                    break;
                }

                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 2:\n  \n");

                if ((Player2.type == "Mages") && !(Player2.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player2.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = random.Next(1, 2);
                }


                switch (actionChoice)
                {
                    case 1:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.getBlockedDamage(Player2.basicAttackDamage);
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                            if (Player2.checkBoost)
                            {
                                Player2.basicAttackDamage -= Player2.attackBoost;
                                Player2.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.abilityHeavyFighters();
                            Player2.uniqueAbility();
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player2.getAttackBoost();
                            Player2.uniqueAbility();
                        }
                        else
                        {
                            if (Player1.type == "HeavyFighters")
                            {
                                Player1.getBlockedDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player1.getDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player1.health <= 0)
                {
                    Console.WriteLine(" \n Игра закончилась, победил Игрок 2 \n");
                    break;
                }
            }
        }

        public static void FightEvE()       // ------------------------------ИИ против ИИ
        {
            FighterSelection selection = new FighterSelection();
            Random random = new Random();

            Console.WriteLine("\nИгрок 1, выберете свой класс:\n");
            int classPlayer1 = random.Next(1, 3);

            var Player1 = FighterSelection.ClassFighterChoiseAI(classPlayer1);

            Console.WriteLine("\nИгрок 1 выбрал персонажа " + Player1.name, "\n");

            Console.WriteLine("\nИгрок 2, выберете свой класс:\n");
            int classPlayer2 = random.Next(1, 3);

            var Player2 = FighterSelection.ClassFighterChoiseAI(classPlayer2);

            Console.WriteLine("\nИгрок 2 выбрал персонажа " + Player2.name, "\n");

            while ((Player1.health >= 0) || (Player2.health >= 0))
            {
                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 1:\n \n");
                int actionChoice = 1;

                if ((Player1.type == "Mages") && !(Player1.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player1.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = random.Next(1, 2);
                }


                switch (actionChoice)
                {
                    case 1:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.getBlockedDamage(Player1.basicAttackDamage);
                        }
                        if (Player1.type == "MartialArts")
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                            if (Player1.checkBoost)
                            {
                                Player1.basicAttackDamage -= Player1.attackBoost;
                                Player1.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player2.getDamage(Player1.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.abilityHeavyFighters();
                            Player1.uniqueAbility();
                        }
                        if (Player1.type == "MartialArts")
                        {
                            Player1.getAttackBoost();
                            Player1.uniqueAbility();
                        }
                        else
                        {
                            if (Player2.type == "HeavyFighters")
                            {
                                Player2.getBlockedDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player2.getDamage(Player1.magicDamage);
                                Player1.uniqueAbility();
                                Player1.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player2.health <= 0)
                {
                    Console.WriteLine(" \nИгра закончилась, победил Игрок 1 \n");
                    break;
                }

                Console.WriteLine($"\n{Player1.name}: Здоровье {Player1.health}, Атака {Player1.basicAttackDamage}\n" +
                    $"{Player2.name}: Здоровье {Player2.health}, Атака {Player2.basicAttackDamage}\n");

                Console.WriteLine("\nХод Игрока 2:\n  \n");

                if ((Player2.type == "Mages") && !(Player2.currentAbilityToUse))
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность. (На этом ходу недоступно)\n" +
                        "Было автоматически выбрано первое действие.\n");
                    Player2.currentAbilityToUse = true;
                    actionChoice = 1;
                }
                else
                {
                    Console.WriteLine("Выберите действие, введя соответствующие число: \n1) Нанести удар.\n2) Использовать способность.");
                    actionChoice = random.Next(1, 2);
                }


                switch (actionChoice)
                {
                    case 1:
                        if (Player1.type == "HeavyFighters")
                        {
                            Player1.getBlockedDamage(Player2.basicAttackDamage);
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                            if (Player2.checkBoost)
                            {
                                Player2.basicAttackDamage -= Player2.attackBoost;
                                Player2.checkBoost = false;
                            }
                        }
                        else
                        {
                            Player1.getDamage(Player2.basicAttackDamage);
                        }
                        break;

                    case 2:
                        if (Player2.type == "HeavyFighters")
                        {
                            Player2.abilityHeavyFighters();
                            Player2.uniqueAbility();
                        }
                        if (Player2.type == "MartialArts")
                        {
                            Player2.getAttackBoost();
                            Player2.uniqueAbility();
                        }
                        else
                        {
                            if (Player1.type == "HeavyFighters")
                            {
                                Player1.getBlockedDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                            else
                            {
                                Player1.getDamage(Player2.magicDamage);
                                Player2.uniqueAbility();
                                Player2.currentAbilityToUse = false;
                            }
                        }
                        break;
                }

                if (Player1.health <= 0)
                {
                    Console.WriteLine(" \n Игра закончилась, победил Игрок 2 \n");
                    break;
                }
            }
        }
    }
}
