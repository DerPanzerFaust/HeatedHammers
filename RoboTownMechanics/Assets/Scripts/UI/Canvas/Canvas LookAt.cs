using UnityEngine;

public class Canvas : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, 
        Camera.main.transform.rotation * Vector3.up);
    }
}
