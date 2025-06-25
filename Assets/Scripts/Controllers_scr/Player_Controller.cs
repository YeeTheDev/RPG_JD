using RPG.Movement;
using UnityEngine;

namespace RPG.Controllers
{
    public class Player_Controller : MonoBehaviour
    {
        bool lastPressedH;
        Vector2 inputAxis;
        Mover mover;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            CalculateInputAxis();
        }

        private void FixedUpdate()
        {
            mover.Move(inputAxis);
        }

        private void CalculateInputAxis()
        {
            bool hHold = Input.GetButton("Horizontal");
            bool vHold = Input.GetButton("Vertical");

            if ((hHold && !vHold) || (Input.GetButtonDown("Horizontal") && vHold)) { lastPressedH = true; }
            if ((!hHold && vHold) || (Input.GetButtonDown("Vertical") && hHold)) { lastPressedH = false; }

            inputAxis.x = lastPressedH ? Input.GetAxisRaw("Horizontal") : 0;
            inputAxis.y = !lastPressedH ? Input.GetAxisRaw("Vertical") : 0;
        }
    }
}