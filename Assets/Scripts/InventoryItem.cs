using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string chooseName
    {
        get
        {
            if (nameArray.Count > 0)
                return ChooseName();
            else
                return null;
        }
    }

    public int itemValue { get { return Random.Range(0, 99); } }

    public string id
    {
        get { return CreateID(); }
    }
    // Start is called before the first frame update
    public InventoryItem()
    {
        string nameChosen = chooseName;
        string idChosen = id;
        int valueChosen = itemValue;
    }

  
    public List<string> nameArray = new List<string>() {"Rose", "Gold Coin", "Halter", "Picnic Basket", "Musical Instrument", "Sewing Needle",
        "Music Box", "Candle", "Summoning Scroll", "Dagger of Dravst", "Shield of Fugheind", "Medical Supplies", "All-Curing Flower" };

    
    string ChooseName()
    {
        int index = 0;
        index = Random.Range(1, nameArray.Count);
        
        string nameOfItem = nameArray[index];
        nameArray.RemoveAt(index);
        return nameOfItem;
    }


    string CreateID()
    {
        int letterVal;
        letterVal = Random.Range(97, 123);
        int numberVal = Random.Range(1, 9999);
        string id = (char)letterVal + numberVal.ToString();
        return id;
    }
}
