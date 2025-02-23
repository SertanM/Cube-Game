using UnityEngine;
using CubeGame.Player;

namespace CubeGame.Camera
{
    internal sealed class IdleCameraState : CameraState
    {
        void CameraState.EnterState(CameraManager camera) { }

        void CameraState.UpdateState(CameraManager camera)
        {
            FollowPlayer(camera);
            CheckChanges(camera);
        }

        private static void FollowPlayer(CameraManager camera)
        {
            Vector3 playerPos = camera.player.transform.position;
            LookDirection lockDirection = camera.gameManager.lookDirection;
            float camDistance = camera.player.playerSettings.camDistance;

            Vector3 nextPos = new Vector3
                (
                    lockDirection == LookDirection.Left ? -camDistance : lockDirection == LookDirection.Right ? camDistance : playerPos.x,
                    playerPos.y,
                    lockDirection == LookDirection.Forward ? -camDistance : lockDirection == LookDirection.Backward ? playerPos.z + camDistance : playerPos.z
                );

            camera.transform.position = nextPos;
        }

        private static void CheckChanges(CameraManager camera)
        {
            if (camera.player.currentState.GetType() != typeof(IdleState))
                return;

            float moveAxis = camera.player.inputManager.GetCamMovement();

            switch (moveAxis)
            {
                case  1f:
                    camera.ChangeState(new RotateCameraLeftState());
                    break;
                case -1f:
                    camera.ChangeState(new RotateCameraRightState());
                    break;
            }
        }

        void CameraState.ExitState(CameraManager camera) { }
    }
}
