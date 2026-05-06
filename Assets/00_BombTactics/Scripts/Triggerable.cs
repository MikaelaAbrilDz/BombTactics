using UnityEngine;

public class Triggerable : MonoBehaviour
{
    [SerializeField] protected bool triggered;

    public void ChangeValue(bool value)
    {
        triggered = value;
        DoTrigger();
    }
    protected virtual void DoTrigger()
    {

    }
}
