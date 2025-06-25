using UnityEngine;

namespace RPG.Dialogues
{
    [System.Serializable]
    public class DialogueData
    {
        [SerializeField] string speakerName;
        [SerializeField] string dialogue;

        public string SpeakerName => speakerName;
        public string Dialogue => dialogue;
    }
}