using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [SerializeField] string charName;
    [SerializeField] int playerLevel = 1;
    [SerializeField] int currentEXP;
    [SerializeField] int[] expToNextLevel;
    [SerializeField] int maxLevel = 100;
    [SerializeField] int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP;
    public int strenght;
    public int defence;
    public int weaponPower;
    public int armorPower;
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;

    private void Awake()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
