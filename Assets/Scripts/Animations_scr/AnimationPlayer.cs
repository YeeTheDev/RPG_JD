using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void MoveAnimation(bool isMoving,  float xAxis, float yAxis)
    {
        animator.SetBool("Moving", isMoving);

        if(Mathf.Abs(xAxis) > Mathf.Epsilon)
        {
            animator.SetFloat("XAxis", xAxis);
            animator.SetFloat("YAxis", 0);
        }
        if (Mathf.Abs(yAxis) > Mathf.Epsilon)
        {
            animator.SetFloat("XAxis", 0);
            animator.SetFloat("YAxis", yAxis);
        }
    }
}
