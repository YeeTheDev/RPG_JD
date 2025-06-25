using RPG.LevelData;
using UnityEngine;
using System.Collections;
using RPG.Controllers;

namespace RPG.Physics
{
    public class PlayerCollisioner : MonoBehaviour
    {
        enum CollisionType { Enter, Stay, Exit }

        public GameObject Interaction { get; private set; }

        Player_Controller controller;

        private void Awake()
        {
            controller = GetComponent<Player_Controller>();
        }

        private void OnTriggerEnter2D(Collider2D other) { ProcessCollision(other.gameObject, CollisionType.Enter); }
        private void OnTriggerExit2D(Collider2D other) { ProcessCollision(other.gameObject, CollisionType.Exit); }

        private void ProcessCollision(GameObject other, CollisionType type)
        {
            if (other.CompareTag("Exit") && type == CollisionType.Enter) { StartCoroutine(Exiting(other)); }
            else if (other.CompareTag("Interactable")) { Interaction = type == CollisionType.Enter ? other : null; }
        }

        private IEnumerator Exiting(GameObject other)
        {
            controller.CanMove(false);

            yield return other.GetComponent<Exit>().StartExit();

            controller.SearchBounds();
            controller.CanMove(true);
        }
    }
}