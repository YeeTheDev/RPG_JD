using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject[] windows;

    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText, hPText, mPText, levelText, expText;
    public Slider[] expSlider;
    public Image[] characterImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber) { windows[i].SetActive(!windows[i].activeInHierarchy); }
            else { windows[i].SetActive(false); }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus()
    {
        UpdateMainStats();

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].charName;
        }
    }
}
