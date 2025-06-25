using RPG.Dialogues;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI dialogueText;
        [SerializeField] GameObject mainPanel;
        [SerializeField] GameObject namePanel;

        DialogManager dialogManager;

        private void Awake() => dialogManager = FindObjectOfType<DialogManager>();

        private void OnEnable()
        {
            dialogManager.OnStartDialogue += ShowPanel;
            dialogManager.OnShowDialogue += ShowDialogue;
            dialogManager.OnEndDialogue += HidePanel;
        }

        private void OnDisable()
        {
            dialogManager.OnStartDialogue -= ShowPanel;
            dialogManager.OnShowDialogue -= ShowDialogue;
            dialogManager.OnEndDialogue -= HidePanel;
        }

        private void ShowPanel() => mainPanel.SetActive(true);

        private void ShowDialogue(DialogueData dialogue)
        {
            namePanel.SetActive(dialogue.SpeakerName != "" && dialogue.SpeakerName != null);
            nameText.text = dialogue.SpeakerName;
            dialogueText.text = dialogue.Dialogue;
        }

        private void HidePanel() => mainPanel.SetActive(false);
    }
}