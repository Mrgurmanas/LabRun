using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class
{
    class Inventory
    {
        private const int SIZE = 3;
        private List<int> inventoryList;

        public Inventory()
        {
            inventoryList = new List<int>();
        }

        public bool CanAddItem()
        {
            return inventoryList.Count < SIZE;
        }

        public bool AddItem(int itemId)
        {
            if(inventoryList.Count < SIZE)
            {
                inventoryList.Add(itemId);
                return true;
            }
            return false;
        }

        public bool RemoveItem()
        {
            if(inventoryList.Count > 0)
            {
                inventoryList.RemoveAt(0);
                return true;
            }
            return false;
        }

        public int UseItem()
        {
            int item = -1;
            if (inventoryList.Count > 0)
            {
                item = inventoryList[0];
                inventoryList.RemoveAt(0);
            }
            return item;
        }

        public int GetItem(int index)
        {
            if(inventoryList.Count > 0 && inventoryList.Count >= (index + 1) && index >= 0 && index < SIZE)
            {
                return inventoryList[index];
            }
            return -1;
        }
    }
}
