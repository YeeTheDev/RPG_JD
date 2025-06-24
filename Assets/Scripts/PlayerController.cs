using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Controllers;
using RPG.LevelData;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    public string areaTransitionName;
    public bool canMove = true;

    Player_Controller playerController;
    AnimationPlayer animationPlayer;
    LevelBounds levelBounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        playerController = GetComponent<Player_Controller>();
        animationPlayer = GetComponent<AnimationPlayer>();

        levelBounds = FindObjectOfType<LevelBounds>();
    }

    void Update()
    {
        if (canMove) { rb.velocity = playerController.ControlAxis * moveSpeed;  Debug.Log("Hello there!"); }
        else { rb.velocity = Vector2.zero; Debug.Log("General Kenobi!"); }

        animationPlayer.MoveAnimation(rb.velocity.sqrMagnitude > Mathf.Epsilon, rb.velocity.x, rb.velocity.y);;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, levelBounds.MinLimit.x, levelBounds.MaxLimit.x),
                                           Mathf.Clamp(transform.position.y, levelBounds.MinLimit.y, levelBounds.MaxLimit.y),
                                           transform.position.z);
    }
}
