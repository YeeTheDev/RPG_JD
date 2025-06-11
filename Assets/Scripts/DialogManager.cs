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

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); } 
    }

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

                        GameManager.instance.dialogActive = false;
                    }
                    else
                    {
                        CheckIfName();

                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else { justStarted = false; } 
            }
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson)
    {
        justStarted = true;
        currentLine = 0;
        dialogLines = newLines;

        CheckIfName();
        dialogText.text = dialogLines[currentLine];
        
        dialogBox.SetActive(true);
        nameBox.SetActive(isPerson);

        GameManager.instance.dialogActive = true;
    }

    public void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}
