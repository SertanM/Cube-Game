using CubeGame.Inputs;
using CubeGame.Player;
using UnityEngine;
using Zenject;

namespace CubeGame.Camera
{
    internal sealed class CameraManager : MonoBehaviour
    {
        private CameraState currentState = null;
        [Inject] internal GameManager gameManager;
        [Inject] internal PlayerManager player;

        private void OnEnable()
        {
            currentState = new IdleCameraState();
            currentState.EnterState(this);
        }

        private void Update() 
            => currentState.UpdateState(this);
        

        internal void ChangeState(CameraState newState)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
    }
}
