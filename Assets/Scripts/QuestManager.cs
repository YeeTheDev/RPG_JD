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
}
