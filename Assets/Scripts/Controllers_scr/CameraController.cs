using RPG.LevelData;
using UnityEngine;

namespace RPG.Controllers
{
    public class CameraController : MonoBehaviour
    {
        Transform target;
        Vector3 bottomLeftLimit;
        Vector3 topRightLimit;

        private void Awake()
        {
            float halfHeight = Camera.main.orthographicSize;
            float halfWidth = halfHeight * Camera.main.aspect;

            LevelBounds levelBounds = FindObjectOfType<LevelBounds>();
            bottomLeftLimit = levelBounds.MinLimit + new Vector3(halfWidth, halfHeight, 0);
            topRightLimit = levelBounds.MaxLimit + new Vector3(-halfWidth, -halfHeight, 0);
        }

        void Start() => target = GameObject.FindGameObjectWithTag("Player").transform;

        void LateUpdate()
        {
            if (target == null) { return; }

            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            ClampPosition();
        }

        private void ClampPosition()
        {
            float clampedXAxis = Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x);
            float clampedYAxis = Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y);
            transform.position = new Vector3(clampedXAxis, clampedYAxis, transform.position.z);
        }
    }
}