using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, battleActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public bool shopActive;
    public int currentGold;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); } 
    }

    // Start is called before the first frame update
    void Start()
    {
        SortItems();
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab) { return referenceItems[i]; }
        }

        return null;
    }

    public void SortItems()
    {
        bool finished = false;

        for (int i = 0; i < itemsHeld.Length && !finished; i++)
        {
            if (itemsHeld[i] == "")
            {
                for (int j = i; j < itemsHeld.Length; j++)
                {
                    if (itemsHeld[j] != "")
                    {
                        itemsHeld[i] = itemsHeld[j];
                        numberOfItems[i] = numberOfItems[j];

                        itemsHeld[j] = "";
                        numberOfItems[j] = 0;

                        break;
                    }
                    else if ((j + 1) >= itemsHeld.Length) { finished = true; }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemIndex = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemIndex = i;
                foundSpace = true;
                break;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for (int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    break;
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemIndex] = itemToAdd;
                numberOfItems[newItemIndex]++;
            }
            else { Debug.LogError($"{itemToAdd} does not exists!"); }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemIndex = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemIndex = i;

                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemIndex]--;

            if (numberOfItems[itemIndex] <= 0)
            {
                itemsHeld[itemIndex] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else { Debug.LogError($"Couldn't find {itemToRemove}"); }
    }
}
