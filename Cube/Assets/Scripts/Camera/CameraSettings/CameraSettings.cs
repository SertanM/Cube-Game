using UnityEngine;

namespace CubeGame.Camera
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Cube Game/CameraSettings")]
    internal sealed class CameraSettings : ScriptableObject
    {
        [Header("---Camera Settings---")]
        [SerializeField] internal float camDistance = 20f;
    }
}