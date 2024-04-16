using UnityEngine;

public class CanvasLookat : MonoBehaviour
{
    //--------------------Functions--------------------//
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, 
        Camera.main.transform.rotation * Vector3.up);
    }
}
