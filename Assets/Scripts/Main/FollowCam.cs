using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Vector3 offset;

    void LateUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("Player")) return;
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + offset;
    }
}