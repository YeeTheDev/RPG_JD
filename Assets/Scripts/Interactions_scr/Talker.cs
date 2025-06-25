using RPG.Dialogues;
using RPG.Interactions;
using UnityEngine;

public class Talker : MonoBehaviour, IInteraction
{
    [SerializeField] DialogueData[] dialogues;

    DialogManager dialogManager;

    private void Awake() => dialogManager = FindObjectOfType<DialogManager>();

    public bool TryInteraction() => dialogManager.TryDialogue(dialogues);
}