using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCharacterWalking()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
    }

    public void OnCharacterIdle()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }

    public void AttackTrigger()
    {
        animator.SetTrigger("Attack");
    }
}
