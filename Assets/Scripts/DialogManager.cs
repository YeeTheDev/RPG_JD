using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject nameBox;
    [SerializeField] string[] dialogLines;
    [SerializeField] int currentLine;

    private void Start()
    {
        dialogText.text = dialogLines[currentLine];
    }

    private void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                currentLine++;

                if (currentLine >= dialogLines.Length) { dialogBox.SetActive(false); }
                else { dialogText.text = dialogLines[currentLine]; }
            }
        }
    }
}
