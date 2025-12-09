using System;
using System.IO;
using System.Xml.Linq;

internal class Program
{

    static string[] tasks = new string[10];
    static int taskCount = 0;
    static bool mark = false;
    static bool del = false;

    static void Menu() //Selection menu
    {
        Console.Clear();
        Console.WriteLine("=====================");
        Console.WriteLine("  TODO list manager");
        Console.WriteLine("=====================\n");
        Console.WriteLine("[1] Add a task");
        Console.WriteLine("[2] View all tasks");
        Console.WriteLine("[3] Mark task as complete");
        Console.WriteLine("[4] Delete a task");
        Console.WriteLine("[5] View incomplete tasks only");
        Console.WriteLine("[6] Export ToDo list");
        Console.WriteLine("[7] Import ToDo list");
        Console.WriteLine("[8] Exit\n");
        Console.Write("Enter your choise: ");

        string? x = Console.ReadLine();

        if (x == "1")
            AddTask();
        else if (x == "2")
            ViewAll();
        else if (x == "3")
        {
            mark = true;
            ViewAll();
        }
        else if (x == "4")
        {
            del = true;
            ViewAll();
        }
        else if (x == "5")
            ViewInc();
        else if (x == "6")
            Export();
        else if (x == "7")
            Import();
        else if (x == "8")
            Environment.Exit(0);
        else
        {
            Console.WriteLine("Invalid input");
            Thread.Sleep(1000);
            Menu();
        }
    }

    static void AddTask() //Add a task 
    {
        Console.Clear();
        if (taskCount >= 10)
        {
            //Say the task list is full and return to menu
            Console.WriteLine("Task list is full!");
            RetMenu();
        }
        else
        {
            Console.Write("Enter a new task: ");
            string newTask = Console.ReadLine();
            tasks[taskCount] = newTask;
            taskCount++;
            Console.WriteLine("Task successfully added");
            RetMenu();
        }


    }

    static void ViewAll()//View all taks
    {
        Console.Clear();
        //Output the tasks with their number infront
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks yet!");
            mark = false;
            del = false;
        }
        else
        {
            for (int i = 0; i < taskCount; i++)
            {
                Console.Write($"{i + 1}. ");
                Console.WriteLine(tasks[i]);
            }
        }
        if (mark == true) //check if you are here to mark as done
        {
            //marking as done
            Console.Write("\nWhat task would you like to mark as complete: ");
            string? x = Console.ReadLine();
            int num = int.Parse(x);
            num--;
            if (num >= 0 && num < taskCount)//check if the input is valid
            {
                if (tasks[num].Contains("[DONE]"))//check if it's already marked as done
                {
                    Console.WriteLine("Task already marked as complete");
                    RetMenu();
                }
                else
                {
                    mark = false;
                    tasks[num] = "[DONE] " + tasks[num];

                    Console.WriteLine("Task successfully marked as complete");
                    RetMenu();
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
                Thread.Sleep(1000);
                ViewAll();
            }
        }
        else if (del == true)//check if you are here to delete
        {
            //deleteing a task
            Console.Write("\nWhat task would you like to delete: ");
            string? y = Console.ReadLine();
            int delNum = int.Parse(y);
            delNum--;
            if (delNum >= 0 && delNum < taskCount)
            {
                //shifts all of the tasks behind the selected one causing that one to be overwriten and the others shift back
                del = false;
                for (int j = delNum; j < taskCount - 1; j++)
                {
                    tasks[j] = tasks[j + 1];
                }
                tasks[taskCount - 1] = null;
                taskCount--;

                Console.WriteLine("Task successfully deleted");
                RetMenu();
            }
            else
            {
                Console.WriteLine("Invalid input");
                Thread.Sleep(1000);
                ViewAll();
            }
        }
        else
        {
            //just viewing
            RetMenu();
        }
    }

    static void ViewInc()
    {
        int count = 0;
        Console.Clear();
        //Output the tasks with their number infront
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks yet!");
        }
        else
        {
            for (int i = 0; i < taskCount; i++)
            {
                if (tasks[i].Contains("[DONE]"))//check if it is marked as done
                {
                    //if marked, skip it
                    count++;
                    continue;
                }
                Console.Write($"{i + 1}. ");
                Console.WriteLine(tasks[i]);
            }
            if (count == taskCount)//check if all tasks are marked done
            {
                Console.WriteLine("All tasks completed! Great job!");
                RetMenu();
            }
        }
        RetMenu();
    }

    static void Export()
    {
        Console.Clear();
        Console.Write("What would you like to name the file: ");
        string file = Console.ReadLine();
        File.WriteAllText($"{file}.txt", "");
        for (int i = 0; i < taskCount; i++)
        {
            File.AppendAllText($"{file}.txt", $"{i + 1}. {tasks[i]}\n");
        }
        Console.WriteLine("File successfully created");
        RetMenu();
    }

    static void Import()
    {
        Console.Clear();
        Console.Write("Enter the name of the file to import (without .txt): ");
        string file = Console.ReadLine();
        string path = $"{file}.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("That file does not exist.");
            RetMenu();
        }

        string[] lines = File.ReadAllLines(path);

        // Reset current tasks
        taskCount = 0;

        foreach (string line in lines)
        {
            string text = line.Trim();
            if (text == "")
                continue;

            // Strip "1. ", "2. " etc. if present
            int dotIndex = text.IndexOf(". ");
            if (dotIndex >= 0)
            {
                text = text.Substring(dotIndex + 2);
            }

            if (taskCount >= tasks.Length)
            {
                Console.WriteLine("Reached the maximum number of tasks. Extra tasks were not imported");
                break;
            }

            tasks[taskCount] = text;
            taskCount++;
        }

        Console.WriteLine($"Imported {taskCount} tasks from file.");
        RetMenu();
    }

    static void RetMenu()
    {
        Console.WriteLine("\nPress ENTER to return to menu");
        Console.ReadLine();
        Menu();
    }

    private static void Main(string[] args)
    {
        Menu();

    }
}