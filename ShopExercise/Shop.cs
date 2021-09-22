using System;
using System.Collections.Generic;
using System.Text;

namespace ShopExercise
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        public Shop(Item[] items)
        {
            _inventory = items;
        }

        public bool Sell(Player player, int option)
        {
            if (player.Gold < _inventory[option].Cost)
            {
                Console.WriteLine("You don't have funds.");
                return false;
            }

            else
            {
                player.Buy(_inventory[option]);
                return true;
            }
        }

        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name + " - " + _inventory[i].Cost + "gp";
            }

            return itemNames;
        }
    }
}
