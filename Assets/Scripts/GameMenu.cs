using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] GameObject[] windows;

    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText, hPText, mPText, levelText, expText;
    public Slider[] expSlider;
    public Image[] characterImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public TextMeshProUGUI statusName, statusHP, statusMP, statusStrength, statusDefence, statusWeapon, statusWpnPower,
        statusArmor, statusArmPower, statusExp;
    public Image statusImage;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public TextMeshProUGUI itemName, itemDescription, useButtonText;

    public static GameMenu instance;

    public GameObject itemCharacterChoiceMenu;
    public TextMeshProUGUI[] itemCharacterChoiceNames;

    public TextMeshProUGUI goldTexT;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (menu.activeInHierarchy) { CloseMenu(); }
            else
            {
                menu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hPText[i].text = $"HP: {playerStats[i].currentHP}/{playerStats[i].maxHP}";
                mPText[i].text = $"MP: {playerStats[i].currentMP}/{playerStats[i].maxMP}";
                levelText[i].text = $"Lvl: {playerStats[i].playerLevel}";
                expText[i].text = $"{playerStats[i].currentEXP}/{playerStats[i].expToNextLevel[playerStats[i].playerLevel]}";
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                characterImage[i].sprite = playerStats[i].charImage;
            }
            else { charStatHolder[i].SetActive(false); }
        }

        goldTexT.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber) { windows[i].SetActive(!windows[i].activeInHierarchy); }
            else { windows[i].SetActive(false); }
        }

        CloseItemCharacterChoice();
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;

        CloseItemCharacterChoice();
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        StatusCharacter(0);

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = $"HP: {playerStats[selected].currentHP}/{playerStats[selected].maxHP}";
        statusMP.text = $"MP: {playerStats[selected].currentMP}/{playerStats[selected].maxMP}";
        statusStrength.text = playerStats[selected].strength.ToString();
        statusDefence.text = playerStats[selected].defence.ToString();
        statusWeapon.text = playerStats[selected].equippedWeapon != "" ? playerStats[selected].equippedWeapon : "NONE";
        statusWpnPower.text = playerStats[selected].weaponPower.ToString();
        statusArmor.text = playerStats[selected].equippedArmor != "" ? playerStats[selected].equippedArmor : "NONE";
        statusArmPower.text = playerStats[selected].armorPower.ToString();
        statusExp.text = $"{playerStats[selected].currentEXP}/{playerStats[selected].expToNextLevel[playerStats[selected].playerLevel]}";
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item item)
    {
        activeItem = item;

        if (activeItem.isItem) { useButtonText.text = "Use"; }
        else if (activeItem.isArmour || activeItem.isWeapon) { useButtonText.text = "Equip"; }

        itemName.text = item.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharacterChoiceNames.Length; i++)
        {
            itemCharacterChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoice() { itemCharacterChoiceMenu.SetActive(false); }

    public void UseItem(int selectecCharacter)
    {
        activeItem.Use(selectecCharacter);
        CloseItemCharacterChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }
}
