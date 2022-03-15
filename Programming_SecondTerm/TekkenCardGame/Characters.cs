using System;
using System.Collections.Generic;
using System.Text;

namespace TekkenCardGame
{
    public abstract class Characters
    {
        public string name;
        public string type;
        public int health;
        public int basicAttackDamage;

        public bool currentAbilityToUse = true;
        public bool checkBoost = false;
        public int attackBoost;
        public int magicDamage;

        public abstract void getBlockedDamage(int dmg);
        public abstract void abilityHeavyFighters();
        public abstract void getAttackBoost();
        public abstract void uniqueAbility();


        public void getDamage(int dmg)
        {
            health -= dmg;
        }

        public void getHealth(int hp)
        {
            health += hp;
        }
        
        public void getHitHeavyFighters(HeavyFighters enemy, int dmg)
        {
            enemy.getBlockedDamage(dmg);
        }

        public Characters(string name, int health, int dmg, string type, int boost, int mdmg)
        {
            this.name = name;
            this.health = health;
            basicAttackDamage = dmg;
            this.type = type;
            attackBoost = boost;
            magicDamage = mdmg;
    }
    }


    public abstract class HeavyFighters : Characters
    {
        public int blockChance;
        public int blockChanceMultiplier;
        public int currentBlockChance = 1;
        public int currentBlockChanceMultiplier = 1;

        public override void getBlockedDamage(int dmg)
        {
            health -= (dmg / currentBlockChance) * currentBlockChanceMultiplier;
            currentBlockChance = 1;
            currentBlockChanceMultiplier = 1;
        }

        public override void abilityHeavyFighters()
        {
            currentBlockChance = blockChance;
            currentBlockChanceMultiplier = blockChanceMultiplier;
        }
        public override void getAttackBoost()
        {
        }

        public HeavyFighters(string name, int health, int dmg, string type,int boost, int mdmg, int block, int blockmult) : base(name, health, dmg, type, boost, mdmg)
        {
            blockChance = block;
            blockChanceMultiplier = blockmult;
        }
    }


    public abstract class MartialArts : Characters
    {
        public override void getBlockedDamage(int dmg)
        {
        }

        public override void abilityHeavyFighters()
        {
        }

        public override void getAttackBoost()
        {
            basicAttackDamage += attackBoost;
            checkBoost = true;
        }

        public MartialArts(string name, int health, int dmg, string type, int boost, int mdmg) : base(name, health, dmg, type, boost, mdmg)
        {

        }
    }


    public abstract class Mages : Characters
    {
        public override void getBlockedDamage(int dmg)
        {
        }

        public override void abilityHeavyFighters()
        {
        }

        public override void getAttackBoost()
        {
        }

        public Mages(string name, int health, int dmg, string type, int boost, int mdmg) : base(name, health, dmg, type, boost, mdmg)
        {
        }
    }


    //----------------------------HeavyFighers---------------------
    public class Jack : HeavyFighters
    {
        public Jack() : base("Jack", 55, 6, "HeavyFighters", 0, 0, 2, 1)
        { }

        public static void description()
        {
            Console.WriteLine($"Jack-7 - имеет 55 здоровья, 6 урона. Способность: блок 50% урона, полученного во время следущего хода противника.");
        }

        public override void uniqueAbility()
        {

        }


    }

    public class Panda : HeavyFighters
    {
        public Panda() : base("Panda", 45, 5, "HeavyFighters", 0, 0, 4, 3)
        { }

        public static void description()
        {
            Console.WriteLine($"Panda - имеет 45 здоровья, 5 урона. Способность: +5 к здоровью и блок 25% урона, полученного во время следущего хода противника.");
        }

        public override void uniqueAbility()
        {
            getHealth(5);
        }
    }

    public class Gigas : HeavyFighters
    {
        public Gigas() : base("Gigas", 57, 6, "HeavyFighters", 0, 0, 4, 3)
        { }

        public static void description()
        {
            Console.WriteLine($"Gigas - имеет 57 здоровья, 6 урона. Способность: блок 25% урона, полученного во время следущего хода противника.");
        }

        public override void uniqueAbility()
        {
            
        }
    }

    public class Bob : HeavyFighters
    {
        public Bob() : base("Bob", 40, 8, "HeavyFighters", 0, 0, 5, 1)
        { }

        public static void description()
        {
            Console.WriteLine($"Bob - имеет 40 здоровья, 8 урона. Способность: блок 80% урона, полученного во время следущего хода противника.");
        }

        public override void uniqueAbility()
        {
            
        }
    }

    public class Kuma : HeavyFighters
    {
        public Kuma() : base("Kuma", 50, 5, "HeavyFighters", 0, 0, 5, 4)
        { }

        public static void description()
        {
            Console.WriteLine($"Kuma - имеет 50 здоровья, 5 урона. Способность: +4 к здоровью и блок 20% урона, полученного во время следущего хода противника.");
        }

        public override void uniqueAbility()
        {
            getHealth(4);
        }
    }

    //----------------------------MartialArts-----------------------
    public class Heihachi : MartialArts
    {
        public Heihachi() : base("Heihachi", 40, 5, "MartialArts", 10, 0)
        { }

        public static void description()
        {
            Console.WriteLine($"Heihachi - имеет 40 здоровья, 5 урона. Способность: +10 к атаке на следующий ход");
        }

        public override void uniqueAbility()
        {
            
        }
    }

    public class Kazuya : MartialArts
    {
        public Kazuya() : base("Kazuya", 38, 8, "MartialArts", 15, 0)
        { }

        public static void description()
        {
            Console.WriteLine($"Kazuya - имеет 38 здоровья, 8 урона. Способность: -5 к здоровью и +15 к атаке на следующий ход");
        }

        public override void uniqueAbility()
        {
            getDamage(5);
        }
    }

    public class King : MartialArts
    {
        public King() : base("King", 48, 5, "MartialArts", 7, 0)
        { }

        public static void description()
        {
            Console.WriteLine($"King - имеет 48 здоровья, 5 урона. Способность: +2 к здоровью и +7 к атаке на следующий ход");
        }

        public override void uniqueAbility()
        {
            getHealth(2);
        }
    }

    public class Bryan : MartialArts
    {
        public Bryan() : base("Bryan", 40, 9, "MartialArts", 20, 0)
        { }

        public static void description()
        {
            Console.WriteLine($"Bryan - имеет 40 здоровья, 9 урона. Способность: -10 к здоровью и +20 к атаке на следующий ход");
        }

        public override void uniqueAbility()
        {
            getDamage(10);
        }
    }

    public class Dragunov : MartialArts
    {
        public Dragunov() : base("Dragunov", 35, 7, "MartialArts", 8, 0)
        { }

        public static void description()
        {
            Console.WriteLine($"Dragunov - имеет 35 здоровья, 7 урона. Способность: +2 к здоровью и +8 к атаке на следующий ход");
        }

        public override void uniqueAbility()
        {
            getHealth(2);
        }
    }

    //----------------------------Mages---------------------------
    public class Zafina : Mages
    {
        public Zafina() : base("Zafina", 42, 5, "Mages", 0, 15)
        { }

        public static void description()
        {
            Console.WriteLine($"Zafina - имеет 42 здоровья, 5 урона. Способность: наносит 15 урона противнику (нельзя использовать подряд)");
        }

        public override void uniqueAbility()
        {
            
        }
    }

    public class DevilJin : Mages
    {
        public DevilJin() : base("DevilJin", 32, 7, "Mages", 0, 10)
        { }

        public static void description()
        {
            Console.WriteLine($"Devil Jin - имеет 32 здоровья, 7 урона. Способность: +3 к здоровью и наносит 10 урона противнику (нельзя использовать подряд)");
        }

        public override void uniqueAbility()
        {
            getHealth(3);
        }
    }

    public class Kazumi : Mages
    {
        public Kazumi() : base("Kazumi", 37, 7, "Mages", 0, 13)
        { }

        public static void description()
        {
            Console.WriteLine($"Kazumi - имеет 37 здоровья, 7 урона. Способность: наносит 13 урона противнику (нельзя использовать подряд)");
        }

        public override void uniqueAbility()
        {
            
        }
    }

    public class Eliza : Mages
    {
        public Eliza() : base("Eliza", 33, 8, "Mages", 0, 7)
        { }

        public static void description()
        {
            Console.WriteLine($"Eliza - имеет 33 здоровья, 8 урона. Способность: +8 к здоровью и наносит 7 урона противнику (нельзя использовать подряд)");
        }

        public override void uniqueAbility()
        {
            getHealth(8);
        }
    }

    public class Akuma : Mages
    {
        public Akuma() : base("Akuma", 45, 9, "Mages", 0, 5)
        { }

        public static void description()
        {
            Console.WriteLine($"Akuma - имеет 45 здоровья, 9 урона. Способность: +5 к здоровью и наносит 5 урона противнику (нельзя использовать подряд)");
        }

        public override void uniqueAbility()
        {
            getHealth(5);
        }
    }


}
