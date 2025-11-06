
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    //Setup for values
    public List<string> nameArray = new List<string>() {"Rose", "Gold Coin", "Halter", "Picnic Basket", "Musical Instrument", "Sewing Needle", "Music Box", "Candle", "Summoning Scroll", "Dagger of Dravst", "Shield of Fugheind", "Medical Supplies", "All-Curing Flower" };

    //Randomizes Item Value
    public int ItemValue { get { return Random.Range(0, 99); } }

    public GameObject myInputField; //TextBox
    string ItemToSearch;

    //List by which we sort our items
    [SerializeField]
    List<InventoryItem> inventoryItems = new List<InventoryItem>();

    //Randomizer for Names
    string ChooseName()
    {
        int index = 0;
        index = Random.Range(0, nameArray.Count-1);
        string nameOfItem = nameArray[index];
        nameArray.RemoveAt(index);
        return nameOfItem;
    }

    //Randomizer for ID
    string CreateID()
    {
        int letterVal;
        letterVal = Random.Range(97, 123);
        int numberVal = Random.Range(1, 9999);
        string id = (char)letterVal + numberVal.ToString();
        return id;
    }

    void Start()
    {
        PopulateInventory();
        
    }

    //Add Values 
    //Note: Reads as none in editor but the details ARE added within each object
    void PopulateInventory()
    {
        while (nameArray.Count > 0)
        {
            InventoryItem item = new InventoryItem();
            item.ID = CreateID();
            item.Name = ChooseName();
            item.ItemValue = this.ItemValue;
            inventoryItems.Add(item);
        }
    }

    //Controls our search/sort keys preferably
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            LinearSearchByName(ItemToSearch);
            ItemToSearch = myInputField.GetComponent<TMP_InputField>().text;
        }
    }

    void LinearSearchByName(string itemName)
    {
        int index = 0;
        while (inventoryItems[index].Name != itemName || index != inventoryItems.Count - 1)
        {
            index++;

            if (index == inventoryItems.Count -1)
            {
                break;
            }

            if (inventoryItems[index].Name == itemName)
            {
                InventoryItem itemFound = inventoryItems[index];
                Debug.Log(itemFound.Name + " Was Found at index" + index);
            }
        }
    }
}
