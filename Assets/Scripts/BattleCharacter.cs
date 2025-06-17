using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailable;
    public string characterName;
    public int currentHP, maxHP, currentMP, maxMP, strength, defence, wpnPower, amrPower;
    public bool hasDied;

    public SpriteRenderer spriteRenderer;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    private bool shouldFade;
    public float fadeSpeed = 1f;

    private void Update()
    {
        if (shouldFade)
        {
            spriteRenderer.color = new Color(Mathf.MoveTowards(spriteRenderer.color.r, 1, fadeSpeed * Time.deltaTime),
                                            Mathf.MoveTowards(spriteRenderer.color.g, 0, fadeSpeed * Time.deltaTime),
                                            Mathf.MoveTowards(spriteRenderer.color.b, 1, fadeSpeed * Time.deltaTime),
                                            Mathf.MoveTowards(spriteRenderer.color.a, 0, fadeSpeed * Time.deltaTime));

            if (spriteRenderer.color.a <= 0) { gameObject.SetActive(false); }
        }
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }
}
