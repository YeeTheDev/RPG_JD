using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string areaToLoad;
    [SerializeField] string areaTransitionName;
    [SerializeField] AreaEntrance areaEntrance;
    [SerializeField] float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    void Awake()
    {
        areaEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;

            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoadAfterFade = true;
            UIFade.instance.FadeToBlack();
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
