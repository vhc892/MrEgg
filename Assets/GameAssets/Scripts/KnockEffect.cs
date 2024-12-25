using UnityEngine;

public class KnockEffect : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayKnockAnimation()
    {
        if (anim != null)
        {
            anim.Play("KnockAnim", -1, 0f);
        }
    }
}
