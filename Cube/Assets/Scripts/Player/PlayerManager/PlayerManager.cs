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
    }
}
