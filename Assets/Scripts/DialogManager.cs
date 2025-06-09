using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] TextMeshProUGUI nameText;
    public GameObject dialogBox;
    [SerializeField] GameObject nameBox;
    [SerializeField] string[] dialogLines;
    [SerializeField] int currentLine;

    private bool justStarted;

    private void Awake() => instance = this;

    private void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        PlayerController.instance.canMove = true;
                    }
                    else { dialogText.text = dialogLines[currentLine]; }
                }
                else { justStarted = false; } 
            }
        }
    }

    public void ShowDialog(string[] newLines)
    {
        justStarted = true;
        currentLine = 0;
        dialogLines = newLines;

        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        PlayerController.instance.canMove = false;
    }
}
