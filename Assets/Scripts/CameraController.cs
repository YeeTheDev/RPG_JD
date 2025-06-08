using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Tilemap map;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private void Awake()
    {
        bottomLeftLimit = map.localBounds.min;
        topRightLimit = map.localBounds.max;
    }

    void Start()
    {
        target = PlayerController.instance.transform;
    }

    void LateUpdate()
    {
        Vector3 followPosition = target.position;
        followPosition.x = Mathf.Clamp(followPosition.x, bottomLeftLimit.x, topRightLimit.x);
        followPosition.y = Mathf.Clamp(followPosition.y, bottomLeftLimit.y, topRightLimit.y);
        followPosition.z = transform.position.z;

        transform.position = followPosition;
    }
}
