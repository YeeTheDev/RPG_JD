using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject audioManager;

    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) { Instantiate(player); }
        if (GameManager.instance == null) { Instantiate(gameManager); }
        if (AudioManager.instance == null) { Instantiate(audioManager); }
    }
}
