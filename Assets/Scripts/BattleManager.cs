using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private bool battleActive;

    public GameObject battleScene;
    public Transform[] playerPoints;
    public Transform[] enemyPoints;

    public BattleCharacter[] playerCharacters;
    public BattleCharacter[] enemyCharacters;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); } 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            BattleStart(new string[] { "Waspy Wasp", "Skeletal Friend" });
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if (!battleActive)
        {
            battleActive = true;

            Vector3 camPosition = Camera.main.transform.position;
            camPosition.z = transform.position.z;
            transform.position = camPosition;
            GameManager.instance.battleActive = true;

            battleScene.SetActive(true);
            AudioManager.instance.PlayBGM(0);
        }
    }
}
