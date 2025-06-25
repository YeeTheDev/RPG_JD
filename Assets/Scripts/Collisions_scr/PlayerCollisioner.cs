using RPG.LevelData;
using UnityEngine;
using System.Collections;
using RPG.Movement;

namespace RPG.Physics
{
    public class PlayerCollisioner : MonoBehaviour
    {
        Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Exit")) { StartCoroutine(Exiting(other.GetComponent<Exit>())); }
        }

        private IEnumerator Exiting(Exit exit)
        {
            yield return exit.StartExit();

            mover.Bounds = FindObjectOfType<LevelBounds>();
        }
    }
}