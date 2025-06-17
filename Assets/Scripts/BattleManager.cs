using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

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

    public BattleMove[] movesList;

    public GameObject enemyAttackEffect;
    public DamageNumber damageText;
    public TextMeshProUGUI[] playersNames, playersHP, playersMP;
    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;
    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;
    public BattleNotification notification;

    public int changeToFlee = 35;

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

                if (!activeBattlers[currentTurn].isPlayer) { StartCoroutine(EnemyMoveCoroutine()); }
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) { NextTurn(); Debug.Log(currentTurn); }
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

            UpdateUIStats();
        }
    }

    public void NextTurn()
    {
        currentTurn = (currentTurn + 1) % activeBattlers.Count;

        turnWaiting = true;
        UpdateBattle();
        UpdateUIStats();
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
                if (activeBattlers[i].isPlayer)
                {
                    activeBattlers[i].spriteRenderer.sprite = activeBattlers[i].deadSprite;
                }
                else
                {
                    activeBattlers[i].EnemyFade();
                }
            }
            else
            {
                if (activeBattlers[i].isPlayer)
                {
                    allPlayersDead = false;
                    activeBattlers[i].spriteRenderer.sprite = activeBattlers[i].aliveSprite;
                }
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
        else
        {
            while (activeBattlers[currentTurn].currentHP == 0)
            {
                if (currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCoroutine()
    {
        turnWaiting = false;

        yield return new WaitForSeconds(1f);

        EnemyAttack();

        yield return new WaitForSeconds(1f);

        NextTurn();
    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();
        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
            {
                players.Add(i);
            }
        }

        int selectedTarget = players[Random.Range(0, players.Count)];

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        int movePower = 0;
        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                Instantiate(movesList[i].effect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);
    }

    public void DealDamage(int target, int movePower)
    {
        float attackPower = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower;
        float defence = activeBattlers[target].defence + activeBattlers[target].amrPower;

        float damage = (attackPower / defence) * movePower * Random.Range(0.95f, 1.05f);
        int damageToDeal = Mathf.RoundToInt(damage);

        activeBattlers[target].currentHP -= damageToDeal;

        Instantiate(damageText, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToDeal);

        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for (int i = 0; i < playersNames.Length; i++)
        {
            if (activeBattlers.Count > i)
            {
                if (activeBattlers[i].isPlayer)
                {
                    BattleCharacter playerData = activeBattlers[i];

                    playersNames[i].gameObject.SetActive(true);
                    playersNames[i].text = playerData.characterName;
                    playersHP[i].text = $"{Mathf.Clamp(playerData.currentHP, 0, int.MaxValue)}/{playerData.maxHP}";
                    playersMP[i].text = $"{Mathf.Clamp(playerData.currentMP, 0, int.MaxValue)}/{playerData.maxMP}";
                }
                else { playersNames[i].gameObject.SetActive(false); }
            }
            else { playersNames[i].gameObject.SetActive(false); }
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget)
    {
        int movePower = 0;
        for (int i = 0; i < movesList.Length; i++)
        {
            if (movesList[i].moveName == moveName)
            {
                Instantiate(movesList[i].effect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

        DealDamage(selectedTarget, movePower);

        uIButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);
        NextTurn();
    }

    public void OpenTargetMenu(string moveName)
    {
        List<int> enemies = new List<int>();

        for (int i = 0; i < activeBattlers.Count; i++)
        {
            if (!activeBattlers[i].isPlayer)
            {
                enemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (enemies.Count > i && activeBattlers[enemies[i]].currentHP > 0)
            {
                targetButtons[i].gameObject.SetActive(true);

                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = enemies[i];
                targetButtons[i].targetName.text = activeBattlers[enemies[i]].characterName;
            }
            else
            {
                targetButtons[i].gameObject.SetActive(false);
            }
        }

        targetMenu.SetActive(true);
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeBattlers[currentTurn].movesAvailable.Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;

                for (int j = 0; j < movesList.Length; j++)
                {
                    if (movesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = movesList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Flee()
    {
        int fleeChance = Random.Range(0, 100);

        if (fleeChance < changeToFlee)
        {
            battleActive = false;
            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();
            notification.notificationText.text = "Couldn't escape!";
            notification.Activate();
        }
    }
}
