using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject uIScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameManager;

    private void OnEnable()
    {
        if (UIFade.instance == null) { Instantiate(uIScreen); }
        if (PlayerController.instance == null) { Instantiate(player); }
        if (GameManager.instance == null) { Instantiate(gameManager); }
    }
}
