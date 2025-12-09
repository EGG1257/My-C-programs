using System;

internal class Game
{
    static string playerName;
    static int health = 100;
    static bool key = false;
    static bool torch = false;
    static bool treasure = false;

    static string[] inventory = new string[3];
    static int items = 0;

    static bool quitGame = false;
    static bool firstBeach = true;

    static void DisplayStatus()
    {
        Console.WriteLine("======================");
        Console.WriteLine("Player: " + playerName);
        Console.WriteLine("Health: " + health);

        Console.WriteLine("Inventory:");
        if (items == 0)
            Console.WriteLine("  (empty)");
        else
            for (int i = 0; i < items; i++)
                Console.WriteLine("  - " + inventory[i]);

        Console.WriteLine("======================\n");
    }

    static void UpdateScreen()
    {
        Console.Clear();
        DisplayStatus();
    }

    static void StartGame()
    {
        Console.Clear();
        Console.WriteLine("============================");
        Console.WriteLine("       THE LOST TREASURE");
        Console.WriteLine("============================\n");

        Console.Write("Your name: ");
        playerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(playerName))
            playerName = "Stranger";

        Console.WriteLine($"\nWelcome, {playerName}!");
        Console.WriteLine("Press ENTER to begin...");
        Console.ReadLine();
        Console.Clear();
    }

    static void Scene1()
    {
        UpdateScreen();

        if (firstBeach)
        {
            Console.WriteLine("You open your eyes on a sandy beach.");
            firstBeach = false;
        }
        else
        {
            Console.WriteLine("You return to the familiar sandy beach.");
        }

        Console.WriteLine("Ahead of you is a dark jungle.");
        Console.WriteLine("\n[1] Go into the jungle");
        Console.WriteLine("[2] Explore the beach");
        Console.WriteLine("[3] Log Status");

        int choice = Choice(1, 3);
        if (choice == 1) Scene2_Jungle();
        else if (choice == 2) Scene2_Beach();
        else
        {
            Log();
            Scene1(); 
        }
    }

    static void Scene2_Jungle()
    {
        UpdateScreen();
        health -= 15;
        UpdateScreen();
        Console.WriteLine("Branches scratch your arms. You lost 15 health.\n");

        if (health <= 0)
        {
            Console.WriteLine("You collapse from the cuts.");
            End("lose");
            return;
        }

        Console.WriteLine("You find an old wooden sign pointing towards two paths");
        Console.WriteLine("In one direction the sign says Cave and the other says Village");
        Console.WriteLine("[1] Enter the cave");
        Console.WriteLine("[2] Head toward a village");
        Console.WriteLine("[3] Log Status");

        int choice = Choice(1, 3);
        if (choice == 1) Scene3_Cave();
        else if (choice == 2) Scene3_Village();
        else
        {
            Log();
            Scene2_Jungle();
        }
    }

    static void Scene2_Beach()
    {
        UpdateScreen();
        Console.WriteLine("You begin searching the beach.\n");

        if (!torch)
        {
            Console.WriteLine("You find a backpack containing a TORCH.");
            torch = AddItem("Torch");
        }
        else
        {
            Console.WriteLine("You see where you found the torch earlier.");
        }

        Console.WriteLine();

        if (!key)
        {
            Console.WriteLine("Under a rowboat seat you find a KEY.");
            key = AddItem("Key");
        }
        else
        {
            Console.WriteLine("The broken rowboat is empty now.");
        }

        Console.WriteLine("You find an old wooden sign pointing towards two paths");
        Console.WriteLine("In one direction the sign says Cave and the other says Village");
        Console.WriteLine("\n[1] Head to the cave");
        Console.WriteLine("[2] Go to the village");
        Console.WriteLine("[3] Log Status");

        int choice = Choice(1, 3);
        if (choice == 1) Scene3_Cave();
        else if (choice == 2) Scene3_Village();
        else
        {
            Log();
            Scene2_Beach();
        }
    }

    static void Scene3_Cave()
    {
        UpdateScreen();
        Console.WriteLine("You reach the entrance of a cold, dark cave.\n");

        if (!torch)
        {
            health -= 40;
            UpdateScreen();
            Console.WriteLine("It's pitch black inside and you don't have a torch.");
            Console.WriteLine("You stumble in the darkness and smash your head on a rock. -40 health.\n");

            if (health <= 0)
            {
                Console.WriteLine("You never wake up.");
                End("lose");
                return;
            }

            Console.WriteLine("Dazed and hurting, you crawl back to the beach.");
            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
            Scene1();
            return;
        }

        Console.WriteLine("Your torch lights the cave as you walk deeper inside.");
        Console.WriteLine("You eventually come into a small stone chamber.\n");

        if (!key)
        {
            health -= 80;
            UpdateScreen();
            Console.WriteLine("In the middle of the chamber is a heavy stone chest, but it is locked.");
            Console.WriteLine("You try to force it open with your hands.");
            Console.WriteLine("A hidden trap triggers! Rocks crash down from above. -80 health.\n");

            if (health <= 0)
            {
                Console.WriteLine("You are crushed under the rocks.");
                End("lose");
                return;
            }

            Console.WriteLine("Bruised and shaken, you manage to escape and make your way back to the beach.");
            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
            Scene1();
            return;
        }

        Console.WriteLine("In the middle of the chamber you see a stone chest.");
        Console.WriteLine("You use your key to unlock the chest...");
        Console.WriteLine("Inside you find the LOST TREASURE!");
        treasure = true;
        End("win");
    }


    static void Scene3_Village()
    {
        UpdateScreen();
        Console.WriteLine("You arrive at a small village.\n");
        Console.WriteLine("A few villagers watch you carefully from a distance.");
        Console.WriteLine("[1] Politely ask for help");
        Console.WriteLine("[2] Run into a hut");
        Console.WriteLine("[3] Explore the beach / Log Status");

        int choice = Choice(1, 3);

        if (choice == 1)
        {
            Console.WriteLine("\nYou greet the villagers and ask for help.");
            Console.WriteLine("After a moment of silence, they decide to trust you.");
            Console.WriteLine("They give you healing herbs. (+20 health)");

            health += 20;
            if (health > 100) health = 100;

            UpdateScreen();
            Console.WriteLine("The villagers gave you herbs and restored your strength. (+20 health)\n");
            Console.WriteLine("They point toward a cave in the distance, saying treasure lies within.\n");

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[1] Head to the cave");
            Console.WriteLine("[2] Return to the beach");

            int next = Choice(1, 2);

            if (next == 1)
            {
                Console.WriteLine("\nYou follow the path toward the cave...");
                Console.WriteLine("Press ENTER...");
                Console.ReadLine();
                Scene3_Cave();
            }
            else
            {
                Console.WriteLine("\nYou decide to return to the beach instead.");
                Console.WriteLine("Press ENTER...");
                Console.ReadLine();
                Scene1();
            }
        }
        else if (choice == 2)
        {
            Console.WriteLine("\nYou shove past the villagers and run into a hut.");
            Console.WriteLine("Inside is a sacred altar. The villagers attack and imprison you.");
            End("lose");
        }
        else
        {
            Log();
            Scene3_Village(); 
        }
    }


    static bool AddItem(string item)
    {
        Console.WriteLine($"You found: {item}");
        bool pick = YesNo("Pick it up?");

        if (!pick)
        {
            UpdateScreen();
            Console.WriteLine($"You left the {item}.\n");
            return false;
        }

        if (items < inventory.Length)
        {
            inventory[items] = item;
            items++;
            UpdateScreen();
            Console.WriteLine($"You picked up: {item}\n");
            return true;
        }

        UpdateScreen();
        Console.WriteLine("Inventory full.\n");
        return false;
    }

    static bool YesNo(string q)
    {
        while (true)
        {
            Console.WriteLine(q);
            Console.WriteLine("[1] Yes");
            Console.WriteLine("[2] No");
            Console.Write("Choice: ");

            string input = Console.ReadLine();
            if (input == "1") return true;
            if (input == "2") return false;

            Console.WriteLine("Invalid.\n");
        }
    }

    static int Choice(int min, int max)
    {
        int c;
        while (true)
        {
            Console.Write("\nChoice: ");
            string inp = Console.ReadLine();

            if (int.TryParse(inp, out c) && c >= min && c <= max)
                return c;

            Console.WriteLine("Invalid choice.");
        }
    }

    static void End(string type)
    {
        Console.WriteLine("\n========== GAME OVER ==========");

        if (type == "win" && treasure)
        {
            Console.WriteLine("You escaped the island with the LOST TREASURE!");
        }
        else
        {
            Console.WriteLine("Your journey ends here.");
        }

        DisplayStatus();
        Console.WriteLine("================================\n");
    }

    static void Log()
    {
        File.WriteAllText(
            $"{playerName}.csv",
            $"Health:{health}, hasKey:{key}, hasTorch:{torch}, treasureFound:{treasure}, itemCount:{items}"
        );
    }

    private static void Main(string[] args)
    {
        bool again = true;

        while (again && !quitGame)
        {
            health = 100;
            key = false;
            torch = false;
            treasure = false;
            items = 0;
            firstBeach = true;

            for (int i = 0; i < inventory.Length; i++)
                inventory[i] = null;

            StartGame();
            Scene1();

            Console.Write("Play again? (Y/N): ");
            string a = Console.ReadLine().ToLower();
            if (a != "y") again = false;
        }

        Console.WriteLine("Thanks for playing!");
        Console.ReadLine();
    }
}
