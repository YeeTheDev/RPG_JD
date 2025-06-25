using System.Collections;
using RPG.Core;
using UnityEngine;

namespace RPG.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] CanvasGroup fadeScreen;
        [SerializeField] float fadeSpeed;

        Loader loader;

        private void Awake() => loader = FindObjectOfType<Loader>();

        private void OnEnable()
        {
            loader.OnStartLoad += StartFade;

            StartFade(false);
        }

        private void OnDisable() => loader.OnStartLoad -= StartFade;

        private void StartFade(bool fadeOut) => StartCoroutine(Fade(fadeOut));

        private IEnumerator Fade(bool fadeOut)
        {
            float target = fadeOut ? 1 : 0;
            float timer = Time.time + fadeSpeed;

            while (Time.time < timer)
            {
                yield return new WaitForEndOfFrame();

                fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, target, Time.deltaTime * fadeSpeed);
            }

            fadeScreen.alpha = target;
        }
    }
}