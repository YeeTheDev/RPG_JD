using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;
    public TextMeshProUGUI xPText, itemText;
    public GameObject rewardScreen;

    public string[] rewardItems;
    public int xpEarned;
    public bool markQuestComplete;
    public string questToMark;

    private void Awake()
    {
        instance = this;
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewardItems = rewards;

        xPText.text = $"Everyone earned {xpEarned} experience.";
        itemText.text = "";

        for (int i = 0; i < rewardItems.Length; i++)
        {
            itemText.text += rewardItems[i] + "\n";
        }

        rewardScreen.SetActive(true);
    }

    public void CloseRewardScreen()
    {
        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddExp(xpEarned);
            }
        }

        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardItems[i]);
        }

        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if (markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
    }
}
