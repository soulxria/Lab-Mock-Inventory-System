
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{

    //Setup for values
    public List<string> nameArray = new List<string>() { "Rose", "Gold Coin", "Halter", "Picnic Basket", "Musical Instrument", "Sewing Needle", "Music Box", "Candle", "Summoning Scroll", "Dagger of Dravst", "Shield of Fugheind", "Medical Supplies", "All-Curing Flower" };

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
        index = Random.Range(0, nameArray.Count - 1);
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

    int SetItemValue()
    {
        return Random.Range(0, 99);
    }

    void Start()
    {
        PopulateInventory();
    }

    //Add Values 
    //Note: Reads as none in editor but the details ARE added within each object
    void PopulateInventory()
    {
        foreach (var name in nameArray)
        {
            GameObject itemObject = new GameObject(name);
            InventoryItem item = itemObject.AddComponent<InventoryItem>();

            item.Name = name;
            item.ID = CreateID();
            item.ItemValue = SetItemValue();
            inventoryItems.Add(item);
        }
    }

    //Controls our search/sort keys preferably
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ItemToSearch = myInputField.GetComponent<TMP_InputField>().text;
            LinearSearchByName(ItemToSearch);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ItemToSearch = myInputField.GetComponent<TMP_InputField>().text;
            BinarySearchByID(ItemToSearch);
        }
    }

    InventoryItem LinearSearchByName(string itemName)
    {
        foreach (var item in inventoryItems)
        {
            if (item.Name == itemName)
            {
                Debug.Log("Item found");
                return item;
            }
        }

        return null;
    }

    InventoryItem BinarySearchByID(string id)
    {
        List<InventoryItem> list = SortListByID();

        int left = 0;
        int right = list.Count - 1;

        return BinarySearchRecursive(list, left, right, id);
    }
    
    List<InventoryItem> SortListByID()
    {
        List<InventoryItem> sortedID = new List<InventoryItem>();
        sortedID = inventoryItems.OrderBy(item => item.ID).ToList();
        return sortedID;
    }

    InventoryItem BinarySearchRecursive(List<InventoryItem> list, int left, int right, string id)
    {
        if (left <= right)
        {
            int mid = left + (right - left) / 2;
            int compare = string.Compare(id, list[mid].ID); // (0 - strings equal) (<0 - first is before second) (>0 - first is after second)

            if (compare == 0)
            {
                Debug.Log("Item found");
                return inventoryItems[mid]; // Found item
            }

            if (compare < 0)
            {
                Debug.Log("Searching Left");
                return BinarySearchRecursive(list, left, mid - 1, id); // Searches left side of list
            }

            if (compare > 0)
            {
                Debug.Log("Searching Right");
                return BinarySearchRecursive(list, mid + 1, right, id); // Searches right side of list
            }
        }

        return null; // Return null if no item found
    }

    public void QuickSortByValue(List<InventoryItem> list, int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(list, low, high);

            QuickSortByValue(list, low, partitionIndex - 1);
            QuickSortByValue(list, partitionIndex + 1, high);
        }
    }

    private int Partition(List<InventoryItem> list, int low, int high)
    {
        int pivot = list[high].ItemValue;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (list[j].ItemValue < pivot)
            {
                i++;
                InventoryItem temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        InventoryItem swapTemp = list[i + 1];
        list[i + 1] = list[high];
        list[high] = swapTemp;

        return i + 1;
    }
}
