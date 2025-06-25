using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] float timeToLoad;

        public IEnumerator StartLoadScene(string sceneToLoad)
        {
            yield return new WaitForSeconds(timeToLoad);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
}