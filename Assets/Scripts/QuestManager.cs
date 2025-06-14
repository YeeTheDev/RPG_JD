using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;


    public string[] questMarkerNames;
    public bool[] questMarkersCompleted;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        questMarkersCompleted = new bool[questMarkerNames.Length];
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogError($"Quest {questToFind}, does not exist.");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumber = GetQuestNumber(questToCheck);
        if (questNumber != 0)
        {
            return questMarkersCompleted[questNumber];
        }

        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkersCompleted[GetQuestNumber(questToMark)] = true;
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersCompleted[GetQuestNumber(questToMark)] = false;
    }
}
