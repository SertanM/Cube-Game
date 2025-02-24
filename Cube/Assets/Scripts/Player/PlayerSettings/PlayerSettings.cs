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

        // Also I think it shouldn't be in here 
        // But I was said I am to lazy for it today
        // And I dont want to get angry with that


    }
}
