using UnityEngine;

namespace CubeGame.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Cube Game / Player Settings")]
    internal sealed class PlayerSettings : ScriptableObject
    {
        // I think it isn't the best way to do this
        // But I dont want to do lot of job today LoL :)

        [Header("---Movement Settings---")]
        [SerializeField] internal float speed = .3f;
        [SerializeField] internal float gravitySpeed = .2f;
        [SerializeField] internal float camRotationSpeed = .4f;

        [Header("---Camera Settings---")]
        [SerializeField] internal float camDistance = 20f;
    }
}
