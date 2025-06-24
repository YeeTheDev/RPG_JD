using UnityEngine;

namespace RPG.LevelData
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] Transform minLimit, maxLimit;

        public Vector3 MinLimit => minLimit.position;
        public Vector3 MaxLimit => maxLimit.position;
    }
}