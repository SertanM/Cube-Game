using UnityEngine;
using CubeGame.Inputs;
using Zenject;

namespace CubeGame.Player
{
    internal sealed class PlayerManager : MonoBehaviour
    {
        internal IPlayerState currentState = null;
        [Inject] internal GameManager gameManager;
        [Inject] internal InputManager inputManager;

        [Header("---Settings---")]
        [SerializeField] internal PlayerSettings playerSettings;

        private void OnEnable()
        {
            currentState = new IdleState();
        }

        private void Update()
            => currentState.UpdateState(this);
        

        internal void ChangeState(IPlayerState newState) 
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }

        internal bool CheckSphere(Vector3 targetPos, GameManager gameManager)
        {
            foreach (var pos in gameManager.positions)
                if (targetPos.x == pos.x || gameManager.lockPos == LockPos.X)
                    if (targetPos.y == pos.y)
                        if (targetPos.z == pos.z || gameManager.lockPos == LockPos.Z)
                            return true;

            return false;
        }
    }
}
