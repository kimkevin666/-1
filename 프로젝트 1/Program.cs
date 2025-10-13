using System;
using System.Collections.Generic;



public class Character
{
    public int Level;
    public string Name;
    public string Job;
    public int BaseAttack;
    public int BaseDefense;
    public int Health;
    public int Gold;
    public int Stamina;
    public int Experience;
    public int Mana;
    public int MaxStamina = 100;
    public int MaxHealth = 200;
    public int MaxMana = 50;

    public Character(int level, string name, string job, int baseAttack, int baseDefense, int health, int gold, int stamina, int experience, int mana)
    {
        Level = level;
        Name = name;
        Job = job;
        BaseAttack = baseAttack;
        BaseDefense = baseDefense;
        Health = health;
        Gold = gold;
        Stamina = stamina;
        Experience = experience;
        Mana = mana;
    }
}


public struct Item 
{
    public string Name;
    public int Type; 
    public int ValueAttack; 
    public int ValueDefense; 
    public string Description;
    public bool IsEquipped; 

   
    
    public Item(string name, int type, int valueAttack,int valueDefense,  string description, bool isEquipped = false)
    {
        Name = name;
        Type = type;
        ValueAttack = valueAttack;
        ValueDefense = valueDefense;
        Description = description;
        IsEquipped = isEquipped;
    }
}


class Program
{
    static Character player;
    static int currentAttack;
    static int currentDefense;

    static List<Item> inventory = new List<Item>();
    static Random random = new Random();

    static void Main(string[] args)
    {
        player = new Character(1, "Chad", "전사", 10, 5, 100, 1500, 50, 0 ,20);

        InitializeInventory();
        CalculateCurrentStats();

        while (true)
        {
            ShowMainMenu();
        }
    }

  
    static void InitializeInventory()
    {
   
        inventory.Add(new Item("무쇠갑옷", 1, 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
        inventory.Add(new Item("낡은 검", 0, 2, 0, "쉽게 볼 수 있는 낡은 검 입니다."));
        inventory.Add(new Item("연습용 창", 0, 3, 0, "검보다는 그대로 창이 다루기 쉽죠."));

        inventory.Add(new Item("가죽 헬멧", 1, 0, 2, "가벼운 가죽으로 만들어진 헬멧입니다."));

        inventory.Add(new Item("반지0", 2, 1, 1, "특별한 마법이 부여된 반지입니다."));
        
    }

 
    static void CalculateCurrentStats()
    {
        currentAttack = player.BaseAttack;
        currentDefense = player.BaseDefense;

        foreach (Item item in inventory)
        {
            if (item.IsEquipped)
            {
                currentAttack += item.ValueAttack;
                currentDefense += item.ValueDefense;
            }
        }
    }


    static void ShowMainMenu()
    {
        Console.Clear();
       
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=======================================");
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("=======================================");
        Console.ResetColor();

        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
 
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 랜덤 모험");
        Console.WriteLine("4. 마을 순찰하기");
        Console.WriteLine("5. 훈련하기");
        Console.ResetColor();

        Console.Write("\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("원하시는 행동을 입력해주세요.");
        Console.ResetColor();
        Console.Write(">> ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":ShowStatus();
                break;

            case "2": ShowInventory();
                break;

            case "3": StartRandomAdventure();
                break;

            case "4": PatrolVillage();
                break;

            case "5": TrainCharacter();
                break;

            default:

                Console.WriteLine();
                Console.WriteLine("- 잘못된 입력입니다.");
                break;
        }
    }

    static void TrainCharacter()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=====================================");
        Console.WriteLine("           **훈련하기**              ");
        Console.WriteLine("=====================================");
        Console.ResetColor();

        int staminaCost = 15;
      
        if (player.Stamina >= staminaCost)
        {
            player.Stamina-=staminaCost;
            Console.WriteLine($"스태미나 {staminaCost}를 소비했습니다. (남은 스태미나: {player.Stamina})");
            Console.WriteLine("\n열심히 훈련합니다...\n");

            int chance = random.Next(1, 101);

            if (chance <= 40)
            {
                int expGain = 50;
                Console.WriteLine("기본기 훈련을 통해 경험치를 얻었습니다.");
                player.Experience += expGain;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n{expGain} 경험치 획득! (현재 경험치: {player.Experience})");
                Console.ResetColor();
            }
            else if (chance <= 70) 
            {
                int goldGain = 800;
                Console.WriteLine("훈련장에서 부수입으로 골드를 발견했습니다.");
                player.Gold += goldGain;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{goldGain} G 획득! (현재 골드: {player.Gold} G)");
                Console.ResetColor();
            }
            else 
            {
                int manaGain = 10;
                Console.WriteLine("정신 집중 훈련을 통해 마나를 회복(획득)했습니다.");
                player.Mana += manaGain;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{manaGain} 마나 획득! (현재 마나: {player.Mana})");
                Console.ResetColor();
            }
        }
        else
        {
            DisplayError($"스태미나가 부족합니다. (필요 스태미나: {staminaCost})");
            return;
        }

        Console.WriteLine("\n계속하려면 아무 키나 누르십시오...");
        Console.ReadKey();

    }


    static void PatrolVillage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=====================================");
        Console.WriteLine("          **마을 순찰**              ");
        Console.WriteLine("=====================================");
        Console.ResetColor();

        int staminaCost = 5;

        if (player.Stamina >= staminaCost)
        {
            player.Stamina -= staminaCost;
            Console.WriteLine($"스태미나 {staminaCost}를 소비했습니다. (남은 스태미나: {player.Stamina})");
            Console.WriteLine("\n마을을 순찰합니다...\n");

            
            int chance = random.Next(1, 101);

            if (chance <= 10) 
            {
                int cost = 500;
                Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                if (player.Gold >= cost)
                {
                    player.Gold -= cost;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{cost} G 소비! (남은 골드: {player.Gold} G)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\n하지만 골드가 부족해서 간식을 사줄 수 없었다...");
                }
            }
            else if (chance <= 20) 
            {
                int reward = 2000;
                Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                player.Gold += reward;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{reward} G 획득! (현재 골드: {player.Gold} G)");

                Console.ResetColor();
            }
            else if (chance <= 40) 
            {
                int reward = 1000;
                int manaGain = 5;
                Console.WriteLine("길 잃은 사람을 안내해주었다. 보상을 얻는다");
                player.Gold += reward;
                player.Mana += manaGain;
                if (player.Mana > player.MaxMana) player.Mana = player.MaxMana;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{reward} G 획득!");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{manaGain} 마나 회복! (현재 마나: {player.Mana} )");
                Console.ResetColor();
            }
            else if (chance <= 70) 
            {
                int reward = 500;
                int staminaGain = 10;
                Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                player.Gold += reward;
                player.Stamina += staminaGain;

                if (player.Stamina > player.MaxStamina) player.Stamina = player.MaxStamina;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{reward} G 획득!");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{staminaGain} 스태미나 회복! (현재 스태미나: {player.Stamina})");
                Console.ResetColor();
            }
            else 
            {
                Console.WriteLine("아무 일도 일어나지 않았다");
            }
        }
        else
        {
            DisplayError("스태미나가 부족합니다. (필요 스태미나: 5)");
            return;
        }

        Console.WriteLine("\n계속하려면 아무 키나 누르십시오...");
        Console.ReadKey();
    }


    static void StartRandomAdventure()
    {
      
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=====================================");
        Console.WriteLine("           **랜덤 모험**             ");
        Console.WriteLine("=====================================");
        Console.ResetColor();

        int staminaCost = 10;
        int goldReward = 500;

        if (player.Stamina >= staminaCost)
        {
            player.Stamina -= staminaCost;
            Console.WriteLine($"스태미나 {staminaCost}를 소비했습니다. (남은 스태미나: {player.Stamina})");

            if (random.Next(0, 2) == 0)
            {
                int expGain = 10;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n몬스터 조우! 치열한 전투 끝에 승리했습니다!");
                Console.WriteLine($"{goldReward} G 획득! {expGain} 경험치 획득!");
                player.Gold += goldReward;
                player.Experience += expGain;
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\n아무 일도 일어나지 않았다...");
                if (random.Next(1, 101) <= 30) 
                      {
                    int damage = random.Next(5, 16); 
                    player.Health -= damage;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"앗! 함정에 빠져 {damage}의 피해를 입었습니다! (남은 체력: {player.Health})");
                    Console.ResetColor();

                    if (player.Health <= 0)
                    {
                        Console.WriteLine("캐릭터가 사망했습니다. 게임 오버!");
                        Console.ReadKey();
                        Environment.Exit(0); 
                    }
                }
            }
        }
        else
        {
            DisplayError("스태미나가 부족합니다. (필요 스태미나: 10)");
            return;
        }

        Console.WriteLine("\n계속하려면 아무 키나 누르십시오...");
        Console.ReadKey();
    }

    static void ShowStatus()
    {
        CalculateCurrentStats();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=====================================");
            Console.WriteLine("           **상태 보기**             ");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {player.Level:D2}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            
            Console.Write($"공격력 : {currentAttack} ");
            if (currentAttack > player.BaseAttack)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"(+{currentAttack - player.BaseAttack})");
                Console.ResetColor();
            } 
            else Console.WriteLine();

            Console.Write($"방어력 : {currentDefense} ");
            if (currentDefense > player.BaseDefense)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"(+{currentDefense - player.BaseDefense})");
                Console.ResetColor();
            }
            else Console.WriteLine();

            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"마 나 : {player.Mana}");
            Console.WriteLine($"스테미나 : {player.Stamina}");
            Console.WriteLine($"경험치 : {player.Experience}");

            Console.Write($"Gold : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{player.Gold} G");
            Console.ResetColor();

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.ResetColor();
            Console.Write(">> ");

            string input = Console.ReadLine();

            if (input == "0") break;
            else Console.WriteLine("- 잘못된 입력입니다."); 
        }
    }

    static void ShowInventory()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=====================================");
            Console.WriteLine("           **인벤 토리**             ");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[아이템 목록]");
            Console.ResetColor();

            DisplayItemList();

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.ResetColor();
            Console.Write(">> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ManageEquipment();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("- 잘못된 입력입니다.");
                    break;
            }
        }
    }

    static void DisplayItemList(bool showIndex = false)
    {
        for (int i = 0; i < inventory.Count; i++) 
        {
            Item item = inventory[i]; 

            string indexText = showIndex ? $"{i + 1}. " : "";
            string equippedText = item.IsEquipped ? "[E]" : "";
            string typeText = (item.Type == 0) ? "공격력" : "방어력";

            Console.Write("- ");

      
            if (showIndex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(indexText);
                Console.ResetColor();
            }

            if (item.IsEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{equippedText,-3} ");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{"",-4}");
            }
            Console.Write($"{item.Name,-12} | {typeText} ");
            Console.ForegroundColor = ConsoleColor.Red; // 수치 색상
            Console.Write($"+{equippedText,-3}");
            Console.ResetColor();
            Console.WriteLine($"| {item.Description}");
        }
    }

            static void ManageEquipment()
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=====================================");
                    Console.WriteLine("          **장착 관리**              ");
                    Console.WriteLine("=====================================");
                    Console.ResetColor();
                    Console.WriteLine("원하시는 아이템 번호를 입력하여 장착/해제할 수 있습니다.");

                    DisplayItemList(true);

                    Console.WriteLine();
                    Console.WriteLine("0. 나가기 (인벤토리로)");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("원하시는 행동을 입력해주세요.");
                    Console.ResetColor();
                    Console.Write(">> ");

                    string input = Console.ReadLine();

                    if (input == "0")
                    {
                        return;
                    }

                    if (int.TryParse(input, out int itemIndex))
                    {
                        // 인덱스가 유효한 범위인지 확인 (1부터 아이템 개수까지)
                        if (itemIndex >= 1 && itemIndex <= inventory.Count)
                        {
                            int actualIndex = itemIndex - 1;

                            Item selectedItem = inventory[actualIndex];
                            selectedItem.IsEquipped = !selectedItem.IsEquipped;

                            inventory[actualIndex] = selectedItem;

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine(selectedItem.IsEquipped ? "아이템이 장착되었습니다." : "아이템 장착이 해제되었습니다.");
                            Console.ResetColor();

                            CalculateCurrentStats();
                        }
                        else
                        {
                            Console.WriteLine("- 잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("- 잘못된 입력입니다.");
                    }
                    Console.WriteLine("계속하려면 아무 키나 누르십시오...");
                    Console.ReadKey();
                }
            }
            static void DisplayError(string message)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
                Console.WriteLine("계속하려면 아무 키나 누르십시오...");
                Console.ReadKey();
            }
        }

//색상을 black로 하면 보이질 안는 건가?

// Console.WriteLine vs \n
//스테미나 회복하는 방법

