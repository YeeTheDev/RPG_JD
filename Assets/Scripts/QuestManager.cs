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

        UpdateLocalQuestsObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersCompleted[GetQuestNumber(questToMark)] = false;

        UpdateLocalQuestsObjects();
    }

    public void UpdateLocalQuestsObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if (questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkersCompleted[i]) { PlayerPrefs.SetInt($"QuestMarker_{questMarkerNames[i]}", 1); }
            else { PlayerPrefs.SetInt($"QuestMarker_{questMarkerNames[i]}", 0); }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey($"QuestMarker_{questMarkerNames[i]}"))
            {
                valueToSet = PlayerPrefs.GetInt($"QuestMarker_{questMarkerNames[i]}");
            }

            if (valueToSet == 0) { questMarkersCompleted[i] = false; }
            else { questMarkersCompleted[i] = true; } 
        }
    }
}
