using RPG.Core;
using UnityEngine;
using System.Collections;

namespace RPG.LevelData
{
    public class Exit : MonoBehaviour
    {
        enum EntranceID { A, B, C, D, E, F, G }

        [SerializeField] string sceneToLoad;
        [SerializeField] Transform destination;
        [SerializeField] EntranceID entranceID;

        Loader loader;

        private void Awake() => loader = FindObjectOfType<Loader>();

        public IEnumerator StartExit()
        {
            DontDestroyOnLoad(gameObject);

            yield return loader.StartLoadScene(sceneToLoad);

            UpdatePlayer(GetOtherExit());

            Destroy(gameObject);
        }

        private void UpdatePlayer(Exit otherExit)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = otherExit.destination.position;
        }

        private Exit GetOtherExit()
        {
            foreach (Exit exit in FindObjectsOfType<Exit>())
            {
                if (exit == this) continue;
                if (exit.entranceID != entranceID) continue;

                return exit;
            }

            return null;
        }
    }
}