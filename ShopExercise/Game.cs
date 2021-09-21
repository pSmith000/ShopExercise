using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShopExercise
{
    public struct Item
    {
        public int Cost;
        public string Name;
    }

    class Game
    {
        private Player _player;
        private Shop _shop;
        private bool _gameOver;
        private int _currentScene = 0;

        public void Run()
        {

        }

        private void Start()
        {
            InitializeItems();

        }

        private void Update()
        {


        }

        private void End()
        {
            
        }

        public void InitializeItems()
        {
            Item sword = new Item { Name = "Sword", Cost = 500 };
            Item shield = new Item { Name = "Shield", Cost = 10 };
            Item healthPotion = new Item { Name = "Health Potion", Cost = 15 };

            
        }

        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                //Print options
                Console.WriteLine(description);

                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + "." + options[i]);
                }
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputRecieved))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        //Set input recieved to be the default value
                        inputRecieved = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //If the player didn't type an int
                else
                {
                    //Set input recieved to be the default value
                    inputRecieved = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
        }

        private void Save()
        {
            //Create a new stream writer
            StreamWriter writer = new StreamWriter("Inventory.txt");

            writer.WriteLine(_currentScene);

            //Save player stats
            _player.Save(writer);

            //Close the writer when done saving
            writer.Close();
        }

        private bool Load()
        {

        }

        private void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
                case 1:
                    DisplayShopMenu();
                    break;
            }

        }

        private void DisplayOpeningMenu()
        {
            int choice = GetInput("Welcome to the RPG Shop Simulator! What would you like to do?", "Start Shopping", "Load Inventory");

            if (choice == 1)
            {
                _currentScene = 1;
            }
            else
            {
                _currentScene = 0;
            }
            
        }

        public string[] GetShopMenuOptions()
        {
            string[] options = new string[] { "Sword", "Shield", "Health Potion", "Save Game", "load Inventory" };

            return options;
            
        }

        private void DisplayShopMenu()
        {
            Console.WriteLine("Your gold: " + _player.Gold);
            Console.WriteLine("Your Inventory: " + _player.Inventory);

            int choice = GetInput("What would you like to purchase?", GetShopMenuOptions());

            
        }

    }
}
