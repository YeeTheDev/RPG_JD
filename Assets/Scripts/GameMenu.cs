using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            menu.SetActive(!menu.activeInHierarchy);
            GameManager.instance.gameMenuOpen = menu.activeInHierarchy;
        }
    }
}
