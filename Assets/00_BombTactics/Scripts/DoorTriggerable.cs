using UnityEngine;

public class DoorTriggerable : Triggerable
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    protected override void DoTrigger()
    {
        if (triggered) anim.SetBool("Open", true);
        else anim.SetBool("Open", false);
    }
}
