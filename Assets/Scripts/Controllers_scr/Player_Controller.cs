using RPG.Movement;
using UnityEngine;
using RPG.Physics;
using RPG.Interactions;
using RPG.LevelData;

namespace RPG.Controllers
{
    public class Player_Controller : MonoBehaviour
    {
        bool canMove = true;
        bool lastPressedH;
        Vector2 inputAxis;

        Mover mover;
        PlayerCollisioner collisioner;

        IInteraction interaction;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            mover = GetComponent<Mover>();
            collisioner = GetComponent<PlayerCollisioner>();

            SearchBounds();
        }

        public void CanMove(bool canMove) => this.canMove = canMove;
        public void SearchBounds() => mover.Bounds = FindObjectOfType<LevelBounds>();

        private void Update()
        {
            CalculateInputAxis();
            ReadInteractionInput();
        }

        private void ReadInteractionInput()
        {
            if (collisioner.Interaction != null && Input.GetMouseButtonUp(0))
            {
                if (interaction == null) { interaction = collisioner.Interaction.GetComponent<IInteraction>(); }

                canMove = interaction.TryInteraction();
            }
        }

        private void FixedUpdate()
        {
            mover.Move(inputAxis);
        }

        private void CalculateInputAxis()
        {
            if (!canMove) { inputAxis = Vector2.zero; return; }

            bool hHold = Input.GetButton("Horizontal");
            bool vHold = Input.GetButton("Vertical");

            if ((hHold && !vHold) || (Input.GetButtonDown("Horizontal") && vHold)) { lastPressedH = true; }
            if ((!hHold && vHold) || (Input.GetButtonDown("Vertical") && hHold)) { lastPressedH = false; }

            inputAxis.x = lastPressedH ? Input.GetAxisRaw("Horizontal") : 0;
            inputAxis.y = !lastPressedH ? Input.GetAxisRaw("Vertical") : 0;
        }
    }
}