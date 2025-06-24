using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RPG.LevelData;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Tilemap map;

    Vector3 bottomLeftLimit;
    Vector3 topRightLimit;
    public int musicIndexToPlay = 0;
    private bool musicStarted;

    private void Awake()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        LevelBounds levelBounds = FindObjectOfType<LevelBounds>();
        bottomLeftLimit = levelBounds.MinLimit + new Vector3(halfWidth, halfHeight, 0);
        topRightLimit = levelBounds.MaxLimit + new Vector3(-halfWidth, -halfHeight, 0);
    }

    void Start()
    {
        target = PlayerController.instance.transform;
    }

    void LateUpdate()
    {
        if (target == null) { return; }

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                           Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                           transform.position.z);

        if (!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicIndexToPlay);
        }
    }
}
