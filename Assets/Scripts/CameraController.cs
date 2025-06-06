using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 followPosition = target.position;
        followPosition.z = transform.position.z;
        transform.position = followPosition;
    }
}
