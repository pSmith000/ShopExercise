using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShopExercise
{
    class Player
    {
        private int _gold;
        private Item[] _inventory;

        

        public int Gold
        {
            get
            {
                return _gold;
            }
        }

        public Player(int gold)
        {
            _gold = gold;
            _inventory = new Item[0];
        }

        public void Buy(Item item)
        {
            //Create a new array with the size of the old array
            Item[] newArray = new Item[ _inventory.Length + 1];

            //Copy the values from the old array
            for (int i = 0; i < _inventory.Length; i++)
            {
                newArray[i] = _inventory[i];
            }

            //set the last index to be the new item
            
            newArray[_inventory.Length] = item;

            _inventory = newArray;

            _gold = _gold - item.Cost;

            //return the new array
            return;
        }

        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;
            }

            return itemNames;
        }

        
        public void Save(StreamWriter writer)
        {
            writer.WriteLine(_gold);

            writer.WriteLine(_inventory.Length);

            for (int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
                writer.WriteLine(_inventory[i].Cost);
            }
            
        }

        
        public bool Load(StreamReader reader)
        {

            int inventoryLength = 0;

            if (!int.TryParse(reader.ReadLine(), out inventoryLength))
            {
                return false;
            }

            _inventory = new Item[inventoryLength];

            for (int i = 0; i < _inventory.Length; i++)
            {
                _inventory[i].Name = reader.ReadLine();

                if (!int.TryParse(reader.ReadLine(), out _inventory[i].Cost))
                {
                    return false;
                }
            }

            return true;

        }
        
        
    }
}
