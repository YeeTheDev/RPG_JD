using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public BattleType[] potentialBattles;

    public bool activateOnEnter, activateOnStay, activateOnExit;
    private bool inArea;
    public float timeBetweenBattles;
    private float betweenBattleCounter;

    public bool cannotFlee;

    public bool deactivateAfterStarting;

    public bool shouldCompleteQuest;
    public string questToComplete;

    private void Start()
    {
        betweenBattleCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
    }

    private void Update()
    {
        if (inArea)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                betweenBattleCounter -= Time.deltaTime;
            }

            if (betweenBattleCounter <= 0)
            {
                betweenBattleCounter = Random.Range(timeBetweenBattles * 0.5f, timeBetweenBattles * 1.5f);
                StartCoroutine(StartBattleCoroutine());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activateOnEnter)
            {
                StartCoroutine(StartBattleCoroutine());
            }
            else
            {
                inArea = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (activateOnEnter)
            {
                StartCoroutine(StartBattleCoroutine());
            }
            else
            {
                inArea = false;
            }
        }
    }

    public IEnumerator StartBattleCoroutine()
    {
        GameManager.instance.battleActive = true;

        int selectedBattle = Random.Range(0, potentialBattles.Length);

        BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
        BattleManager.instance.rewardXP = potentialBattles[selectedBattle].rewawrdExp;

        yield return new WaitForSeconds(1.5f);

        BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies, cannotFlee);

        if (deactivateAfterStarting)
        {
            gameObject.SetActive(false);
        }

        BattleReward.instance.markQuestComplete = shouldCompleteQuest;
        BattleReward.instance.questToMark = questToComplete;
    }
}
