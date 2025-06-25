using System;
using UnityEngine;

namespace RPG.Dialogues
{
    public class DialogManager : MonoBehaviour
    {
        public Action OnStartDialogue, OnEndDialogue;
        public Action<DialogueData> OnShowDialogue;

        private int currentDialogueIndex;
        private bool started;

        public bool TryDialogue(DialogueData[] dialogues)
        {
            if (currentDialogueIndex >= dialogues.Length) { EndDialogue(); return true; }
            if (!started) { StartDialogue(); }

            ShowDialog(dialogues);

            return false;
        }

        private void StartDialogue()
        {
            OnStartDialogue?.Invoke();
            started = true;
        }

        public void ShowDialog(DialogueData[] dialogues)
        {
            OnShowDialogue?.Invoke(dialogues[currentDialogueIndex]);
            currentDialogueIndex++;
        }

        private void EndDialogue()
        {
            started = false;
            currentDialogueIndex = 0;
            OnEndDialogue?.Invoke();
        }
    }
}