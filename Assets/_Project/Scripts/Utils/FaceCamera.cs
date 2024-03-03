using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField]
    private Camera cameraLookAt;

    void Start()
    {
        if (cameraLookAt == null)
            cameraLookAt = Camera.main;
    }


    void Update()
    {
        transform.LookAt(transform.position + cameraLookAt.transform.rotation * Vector3.forward,cameraLookAt.transform.rotation * Vector3.up);
    }
}
