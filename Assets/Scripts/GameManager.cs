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

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || battleActive)
        {
            PlayerController.instance.canMove = false;
        }
        else { PlayerController.instance.canMove = true; }
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

    public void SaveData()
    {
        PlayerPrefs.SetString($"Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        for (int i = 0; i < playerStats.Length; i++)
        {
            string startPath = $"Player_{playerStats[i].charName}_";
            if (playerStats[i].gameObject.activeInHierarchy) { PlayerPrefs.SetInt(startPath + "Active", 1); }
            else { PlayerPrefs.SetInt(startPath +"Active", 0); }

            PlayerPrefs.SetInt(startPath + "Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt(startPath + "CurrentExp", playerStats[i].currentEXP);
            PlayerPrefs.SetInt(startPath + "CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt(startPath + "MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt(startPath + "CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt(startPath + "MaxMP", playerStats[i].maxMP);
            PlayerPrefs.SetInt(startPath + "Strength", playerStats[i].strength);
            PlayerPrefs.SetInt(startPath + "Defence", playerStats[i].defence);
            PlayerPrefs.SetInt(startPath + "WpnPwr", playerStats[i].weaponPower);
            PlayerPrefs.SetInt(startPath + "ArmPwr", playerStats[i].armorPower);
            PlayerPrefs.SetString(startPath + "EquippedWpn", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString(startPath + "EquippedArm", playerStats[i].equippedArmor);
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString($"Slot_Item_{i}", itemsHeld[i]);
            PlayerPrefs.SetInt($"Slot_Amount_{i}", numberOfItems[i]);
        }
    }

    public void LoadData()
    {
        Vector3 loadedPosition = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"),
                                        PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
        PlayerController.instance.transform.position = loadedPosition;

        for (int i = 0; i < playerStats.Length; i++)
        {
            string startPath = $"Player_{playerStats[i].charName}_";
            if (PlayerPrefs.GetInt(startPath + "Active") == 0) { playerStats[i].gameObject.SetActive(false); }
            else { playerStats[i].gameObject.SetActive(true); }

            playerStats[i].playerLevel = PlayerPrefs.GetInt(startPath + "Level");
            playerStats[i].currentEXP = PlayerPrefs.GetInt(startPath + "CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt(startPath + "CurrentHP");
            playerStats[i].maxHP = PlayerPrefs.GetInt(startPath + "MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt(startPath + "CurrentMP");
            playerStats[i].maxMP = PlayerPrefs.GetInt(startPath + "MaxMP");
            playerStats[i].strength = PlayerPrefs.GetInt(startPath + "Strength");
            playerStats[i].defence = PlayerPrefs.GetInt(startPath + "Defence");
            playerStats[i].weaponPower = PlayerPrefs.GetInt(startPath + "WpnPwr");
            playerStats[i].armorPower = PlayerPrefs.GetInt(startPath + "ArmPwr");
            playerStats[i].equippedWeapon = PlayerPrefs.GetString(startPath + "EquippedWpn");
            playerStats[i].equippedArmor = PlayerPrefs.GetString(startPath + "EquippedArm");
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString($"Slot_Item_{i}");
            numberOfItems[i] = PlayerPrefs.GetInt($"Slot_Amount_{i}");
        }
    }
}
