using UnityEngine;

namespace CubeGame.Inputs
{
    internal sealed class InputManager : MonoBehaviour
    {
        private PlayerInputs playerInputs;

        private void OnEnable()
        {
            playerInputs = new PlayerInputs();
            playerInputs.Enable();
            playerInputs.Player.Enable();
        }

        internal float GetMovement()
            => playerInputs.Player.Move.ReadValue<float>();

        internal float GetCamMovement()
            => playerInputs.Player.CamMove.ReadValue<float>();
    }
}
