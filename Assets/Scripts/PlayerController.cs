using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    public string areaTransitionName;
    public bool canMove = true;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

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

        if (canMove) { rb.velocity = new Vector2(xAxis, yAxis) * moveSpeed; }
        else { rb.velocity = Vector2.zero; }

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);

        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
        {
            if (canMove)
            {
                animator.SetFloat("lastMoveX", xAxis);
                animator.SetFloat("lastMoveY", yAxis);
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                           Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                           transform.position.z);
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftLimit = bottomLeft + new Vector3(0.5f, 1f, 0);
        topRightLimit = topRight + new Vector3(-0.5f, -1f, 0);
    }
}
