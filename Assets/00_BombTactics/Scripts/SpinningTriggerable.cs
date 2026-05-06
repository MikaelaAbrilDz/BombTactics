using UnityEngine;

public class SpinningTriggerable : Triggerable
{
    [SerializeField] HingeJoint spinner;

    private void Start()
    {
        spinner = GetComponent<HingeJoint>();
    }
    protected override void DoTrigger()
    {
        if (triggered) spinner.useMotor = true;
        else
        {
            spinner.useMotor = false;
            spinner.GetComponent<Rigidbody>().angularVelocity = spinner.GetComponent<Rigidbody>().angularVelocity * 0.1f;
        } 
    }
}
