using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleTargetButton : MonoBehaviour
{
    public string moveName;
    public int activeBattlerTarget;
    public TextMeshProUGUI targetName;

    public void Press()
    {
        BattleManager.instance.PlayerAttack(moveName, activeBattlerTarget);
    }
}
