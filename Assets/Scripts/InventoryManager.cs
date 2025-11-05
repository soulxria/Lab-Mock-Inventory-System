using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public string ItemToSearch;
    // Start is called before the first frame update
    public InventoryItem itemMaker;

    List<InventoryItem> inventoryItems = new List<InventoryItem>();
    void Start()
    {
        while (itemMaker.nameArray.Count > 0)
        {
            InventoryItem item = new InventoryItem();
            inventoryItems.Add(item);
        }
        if(Input.GetKeyDown(KeyCode.L) && ItemToSearch != null)
            {
                LinearSearchByName(ItemToSearch);
            }
    }

    InventoryItem LinearSearchByName(string itemName)
    {
        int index = 0;
        while (inventoryItems[index].chooseName != itemName || index != inventoryItems.Count - 1)
        {
            index++;
        }
        if (index == inventoryItems.Count -1)
        {
            return null;
        }
        InventoryItem itemFound = inventoryItems[index];
        return itemFound;
    }
}
