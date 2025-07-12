using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // STOMP con tecla Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Stomp");
        }

        // KICK con tecla X
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Kick");
        }
    }
}
