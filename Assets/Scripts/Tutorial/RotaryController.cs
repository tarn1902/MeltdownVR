using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryController : MonoBehaviour
{
    public enum Anim
    {
        Roll,
        Bounce,
        Spin,
        Spin2
    }

    public Anim anim;

    public ButtonGame2 button;
    private Animator animator;

    public float Speed;
    public float Multiplyer;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        switch (anim)
        {
            case Anim.Bounce:
                animator.SetTrigger("Bounce");
                break;

            case Anim.Spin:
                animator.SetTrigger("Spin");
                break;

            case Anim.Spin2:
                animator.SetTrigger("Spin2");
                break;
        }
    }

    void Update()
    {
        if (button != null)
        {
            if (button.IsOnFire)
            {
                animator.SetFloat("Speed", Multiplyer);
            }
            else
            {
                animator.SetFloat("Speed", Speed);
            }
        }
        else
        {
            animator.SetFloat("Speed", Speed);
        }
    }
}
