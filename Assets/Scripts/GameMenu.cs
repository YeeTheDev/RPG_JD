using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText, hPText, mPText, levelText, expText;
    public Slider[] expSlider;
    public Image[] characterImage;
    public GameObject[] charStatHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            menu.SetActive(!menu.activeInHierarchy);
            UpdateMainStats();
            GameManager.instance.gameMenuOpen = menu.activeInHierarchy;
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
}
