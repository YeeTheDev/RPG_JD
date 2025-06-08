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
    private float halfHeight;
    private float halfWidth;


    private void Awake()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0);
        topRightLimit = map.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0);
    }

    void Start()
    {
        target = PlayerController.instance.transform;

        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                           Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                           transform.position.z);

    }
}
