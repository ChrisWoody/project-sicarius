using UnityEngine;

public class PlayerRotateArm : MonoBehaviour
{
    //public Transform Crosshair;

    private void Update()
    {
        var crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshairPos.z = transform.position.z;

        var dir = transform.position - crosshairPos;
        transform.up = dir.normalized;

        //Crosshair.position = crosshairPos;
    }
}