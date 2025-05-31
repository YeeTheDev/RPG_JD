using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(xAxis, yAxis) * moveSpeed;
    }
}
