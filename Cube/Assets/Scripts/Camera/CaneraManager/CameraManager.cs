using CubeGame.Inputs;
using CubeGame.Player;
using UnityEngine;
using Zenject;

namespace CubeGame.Camera
{
    internal sealed class CameraManager : MonoBehaviour
    {
        private ICameraState currentState = null;
        [Inject] internal GameManager gameManager;
        [Inject] internal PlayerManager player;

        [Header("---Settings---")]
        [SerializeField] internal CameraSettings cameraSettings;

        private void OnEnable()
        {
            currentState = new IdleCameraState();
            currentState.EnterState(this);
        }

        private void Update() 
            => currentState.UpdateState(this);
        

        internal void ChangeState(ICameraState newState)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
    }
}
