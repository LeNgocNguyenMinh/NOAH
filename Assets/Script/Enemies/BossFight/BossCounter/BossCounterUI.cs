using UnityEngine;

public class BossCounterUI : MonoBehaviour
{
    private bool animFinish = false;
    private void Start()
    {
        animFinish = false;   
    }
    public void SetAnimFinishTrue()
    {
        animFinish = true;
    }
    public void SetAnimFinishFalse()
    {
        animFinish = false;
    }
    public bool GetAnimFinish()
    {
        return animFinish;
    }
}
