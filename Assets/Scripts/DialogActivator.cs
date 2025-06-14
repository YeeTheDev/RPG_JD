using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] string[] lines;
    [SerializeField] bool isPerson = true;

    private bool canActivate;
    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { canActivate = true; }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { canActivate = false; }
    }

    private void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
        }
    }
}
