using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("General Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStrength;

    [Header("Equippable Details")]
    public int weaponStrength;
    public int armorStrength;

    public void Use(int characterToUseOm)
    {
        CharStats selectCharacter = GameManager.instance.playerStats[characterToUseOm];
        string previousEquippedItem = "";

        if (isItem)
        {
            if (affectHP) { selectCharacter.currentHP = Mathf.Clamp(selectCharacter.currentHP + amountToChange, 0, selectCharacter.maxHP); }
            else if (affectMP) { selectCharacter.currentMP = Mathf.Clamp(selectCharacter.currentMP + amountToChange, 0, selectCharacter.maxMP); }
            else if (affectStrength) { selectCharacter.strength += amountToChange; }
        }
        else if (isWeapon)
        {
            if (selectCharacter.equippedWeapon != "") { previousEquippedItem = selectCharacter.equippedWeapon; }

            selectCharacter.equippedWeapon = itemName;
            selectCharacter.weaponPower = weaponStrength;
        }
        else if (isArmour)
        {
            if (selectCharacter.equippedWeapon != "") { previousEquippedItem = selectCharacter.equippedArmor; }

            selectCharacter.equippedArmor = itemName;
            selectCharacter.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
        if (previousEquippedItem != "") { GameManager.instance.AddItem(previousEquippedItem); }
    }
}
