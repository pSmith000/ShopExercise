using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShopExercise
{
    /// <summary>
    /// a public struct
    /// </summary>
    public struct Item
    {
        public int Cost;
        public string Name;
    }

    class Game
    {
        /// <summary>
        /// Initializing the main variables 
        /// </summary>
        private Player _player;
        private Shop _shop;
        private bool _gameOver;
        private int _currentScene = 0;

        public void Run()
        {
            //The main game loop
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }


        /// Initializes the Items
        private void Start()
        {
            InitializeItems();
        }

        ///Displays the current scenes 
        private void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        private void End()
        {
            Console.WriteLine("See you again!");
        }

        /// <summary>
        /// Initializes the items
        /// </summary>
        public void InitializeItems()
        {
            //All the items sold at the shop
            Item sword = new Item { Name = "Sword", Cost = 500 };
            Item shield = new Item { Name = "Shield", Cost = 10 };
            Item healthPotion = new Item { Name = "Health Potion", Cost = 15 };

            //Puts the items in an array
            Item[] _items = new Item[] { sword, shield, healthPotion };

            //Makes a new Shop with the items in it
            _shop = new Shop(_items);

            
        }

        /// <summary>
        /// Gets input from the User
        /// </summary>
        /// <param name="description"></param>
        /// <param name="options">Any number of options</param>
        /// <returns>returns the value of what the user chooses</returns>
        int GetInput(string description, params string[] options)
        {
            //initialize variables
            string input = "";
            int inputRecieved = -1;

            //While the user doesn't give a valid input...
            while (inputRecieved == -1)
            {
                //Print options
                Console.WriteLine(description);

                //For loop to print all the options
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

            //Returns the value
            return inputRecieved;
        }

        /// <summary>
        /// Creates a new writer and writes the player to save
        /// </summary>
        private void Save()
        {
            //Create a new stream writer
            StreamWriter writer = new StreamWriter("Inventory.txt");

            //Save player stats
            _player.Save(writer);

            //Close the writer when done saving
            writer.Close();
        }

        /// <summary>
        /// Loads gold first then loads the items the player has
        /// </summary>
        /// <returns></returns>
        private bool Load()
        {
            bool loadSuccessful = true;

            //If the file doesn't exist...
            if (!File.Exists("Inventory.txt"))
            {
                //...the load fails
                loadSuccessful = false;
            }

            //Create a new stream reader
            StreamReader reader = new StreamReader("Inventory.txt");

            int _gold;

            if (!int.TryParse(reader.ReadLine(), out _gold))
            {
                loadSuccessful = false;
            }

            _player = new Player(_gold);

            if (!_player.Load(reader))
            {
                loadSuccessful = false;
            }

            reader.Close();

            return loadSuccessful;

            
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

            if (choice == 0)
            {
                _currentScene = 1;
                _player = new Player(100);
            }
            else if (choice == 1)
            {
                if (Load())
                {
                    Console.WriteLine("Load Successful");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = 1;
                }
                else
                {
                    Console.WriteLine("Load Failed.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            
        }

        public string[] GetShopMenuOptions()
        {
            string[] options = new string[_shop.GetItemNames().Length]; 

            for (int i = 0; i < _shop.GetItemNames().Length; i++)
            {
                options[i] = _shop.GetItemNames()[i];
            }

            string[] newArray = new string[options.Length + 2];

            //Copy the values from the old array
            for (int i = 0; i < options.Length; i++)
            {
                newArray[i] = options[i];
            }

            //set the last index to be the new item

            newArray[options.Length] = "Save Game";
            newArray[options.Length + 1] = "Quit Game";

            options = newArray;

            return options;
            
        }

        private void DisplayShopMenu()
        {
            Console.WriteLine("Your gold: " + _player.Gold);
            Console.WriteLine("Your Inventory: ");

            string[] playerInventory = _player.GetItemNames();

            for (int i = 0; i < playerInventory.Length; i++)
            {
                Console.WriteLine(playerInventory[i]);
            }

            int choice = GetInput("\nWhat would you like to purchase?", GetShopMenuOptions());

            if (choice == 0)
            {
                _shop.Sell(_player, 0);
            }
            else if (choice == 1)
            {
                _shop.Sell(_player, 1);
            }
            else if (choice == 2)
            {
                _shop.Sell(_player, 2);
            }
            else if (choice == 3)
            {
                Save();
                Console.WriteLine("Saved Game");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (choice == 4)
            {
                _gameOver = true;
            }


            
        }

    }
}