using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class Loader : MonoBehaviour
    {
        public Action<bool> OnStartLoad;

        [SerializeField] float timeToLoad;

        public IEnumerator StartLoadScene(string sceneToLoad)
        {
            OnStartLoad?.Invoke(true);

            yield return new WaitForSeconds(timeToLoad);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}