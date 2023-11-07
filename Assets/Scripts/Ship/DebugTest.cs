using UnityEngine;

public class DebugTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
            InputReceiver.IsMovementBlocked = !InputReceiver.IsMovementBlocked;
    }
}