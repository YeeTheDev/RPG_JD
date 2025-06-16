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

    public List<BattleCharacter> activeBattlers = new List<BattleCharacter>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uIButtonsHolder;

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
            BattleStart(new string[] { "Skeletal Friend", "Waspy Wasp", "Eyeball" });
        }

        if (battleActive)
        {
            if (turnWaiting)
            {
                uIButtonsHolder.SetActive(activeBattlers[currentTurn].isPlayer);
            }
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

            for (int i = 0; i < playerPoints.Length; i++)
            {
                if (GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for (int j = 0; j < playerCharacters.Length; j++)
                    {
                        if (playerCharacters[j].characterName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleCharacter charInstance = Instantiate(playerCharacters[j], playerPoints[i].position,
                                                            playerPoints[i].rotation, playerPoints[i]);

                            activeBattlers.Add(charInstance);

                            CharStats stats = GameManager.instance.playerStats[i];
                            activeBattlers[i].maxHP = stats.maxHP;
                            activeBattlers[i].currentHP = stats.currentHP;
                            activeBattlers[i].maxMP = stats.maxMP;
                            activeBattlers[i].currentMP = stats.currentMP;
                            activeBattlers[i].strength = stats.strength;
                            activeBattlers[i].defence = stats.defence;
                            activeBattlers[i].wpnPower = stats.weaponPower;
                            activeBattlers[i].amrPower = stats.armorPower;
                        }
                    }
                }
            }

            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if (enemiesToSpawn[i] != "" && enemiesToSpawn[i] != null)
                {
                    for (int j = 0; j < enemyCharacters.Length; j++)
                    {
                        if (enemyCharacters[j].characterName == enemiesToSpawn[i])
                        {
                            BattleCharacter enemy = Instantiate(enemyCharacters[j], enemyPoints[i].position,
                                                        enemyPoints[i].rotation, enemyPoints[i]);

                            activeBattlers.Add(enemy);
                        }
                    }
                }
            }

            turnWaiting = true;
            currentTurn = 0;
        }
    }

    public void NextTurn()
    {
        currentTurn = (currentTurn + 1) % activeBattlers.Count;

        turnWaiting = true;
        UpdateBattle();
    }

    public void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].currentHP < 0)
            {
                activeBattlers[i].currentHP = 0;
            }

            if (activeBattlers[i].currentHP == 0)
            {

            }
            else
            {
                if (activeBattlers[i].isPlayer) { allPlayersDead = false; }
                else { allEnemiesDead = false; } 
            }
        }

        if (allEnemiesDead || allPlayersDead)
        {
            if (allEnemiesDead)
            {
            }
            else
            {
            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;
        }
    }
}
