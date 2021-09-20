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

        public Item[] Inventory
        {
            get
            {
                return _inventory;
            }
        }

        public Player(int gold)
        {
            _gold = gold;
        }

        public void Buy(Item item)
        {
            //Create a new array with the size of the old array
            Item[] newArray = new Item[_inventory.Length + 1];

            //Copy the values from the old array
            for (int i = 0; i < _inventory.Length; i++)
            {
                newArray[i] = _inventory[i];
            }

            //set the last index to be the new item
            ///Commented out code
            //newArray[newArray.Length - 1] = value;

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

        /*
        public override void Save(StreamWriter writer)
        {
            writer.WriteLine(_gold);
            base.Save(writer);
            writer.WriteLine(_inventory);
        }

        public override bool Load(StreamReader reader)
        {
            //if the base loading function fails...
            if (!base.Load(reader))
            {
                //...return false
                return false;
            }

            //If the current line can't be converte into an int...
            if (!int.TryParse(reader.ReadLine(), out _inventory))
            {
                //...return false
                return false;
            }

            //Return whether or not the item was equipped successfully
            return true;
            */
        
    }
}
