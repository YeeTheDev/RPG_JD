using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject uIScreen;
    [SerializeField] GameObject player;

    private void OnEnable()
    {
        if (UIFade.instance == null) { Instantiate(uIScreen); }
        if(PlayerController.instance == null) { Instantiate(player); }
    }
}
