using UnityEngine;

namespace RPG.Controllers
{
    public class Player_Controller : MonoBehaviour
    {
        public Vector2 ControlAxis { get; private set; }

        bool lastPressedH;

        private void Update()
        {
            CalculateInputAxis();
        }

        private void CalculateInputAxis()
        {
            Vector2 axis = new Vector2();

            bool hHold = Input.GetButton("Horizontal");
            bool vHold = Input.GetButton("Vertical");

            if ((hHold && !vHold) || (Input.GetButtonDown("Horizontal") && vHold)) { lastPressedH = true; }
            if ((!hHold && vHold) || (Input.GetButtonDown("Vertical") && hHold)) { lastPressedH = false; }

            axis.x = lastPressedH ? Input.GetAxisRaw("Horizontal") : 0;
            axis.y = !lastPressedH ? Input.GetAxisRaw("Vertical") : 0;

            ControlAxis = axis;
        }
    }
}