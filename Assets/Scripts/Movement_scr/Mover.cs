using System.Collections;
using RPG.LevelData;
using UnityEngine;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 2f;

        public LevelBounds Bounds;

        Rigidbody2D rb2D;
        AnimationPlayer animationPlayer;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animationPlayer = GetComponent<AnimationPlayer>();
        }

        public void Move(Vector2 axis)
        {
            rb2D.velocity = axis * moveSpeed;

            animationPlayer.MoveAnimation(rb2D.velocity.sqrMagnitude > Mathf.Epsilon, rb2D.velocity.x, rb2D.velocity.y); ;

            ClampPosition();
        }

        private void ClampPosition()
        {
            float clampedXAxis = Mathf.Clamp(transform.position.x, Bounds.MinLimit.x, Bounds.MaxLimit.x);
            float clampedYAxis = Mathf.Clamp(transform.position.y, Bounds.MinLimit.y, Bounds.MaxLimit.y);
            transform.position = new Vector3(clampedXAxis, clampedYAxis, transform.position.z);
        }
    }
}