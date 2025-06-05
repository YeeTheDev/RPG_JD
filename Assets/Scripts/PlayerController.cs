using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    public static PlayerController instance;
    public string areaTransitionName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(xAxis, yAxis) * moveSpeed;

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            animator.SetFloat("lastMoveX", xAxis);
            animator.SetFloat("lastMoveY", yAxis);
        }
    }
}
