using UnityEngine;
using CubeGame.Player;

namespace CubeGame.Camera
{
    internal sealed class IdleCameraState : ICameraState
    {
        void ICameraState.EnterState(CameraManager camera) { }

        void ICameraState.UpdateState(CameraManager camera)
        {
            FollowPlayer(camera);
            CheckChanges(camera);
        }

        private static void FollowPlayer(CameraManager camera)
        {
            Vector3 playerPos = camera.player.transform.position;
            LookDirection lookDirection = camera.gameManager.lookDirection;
            float camDistance = camera.cameraSettings.camDistance;

            Vector3 nextPos = new Vector3
                (
                    playerPos.x + (lookDirection == LookDirection.Left ? -camDistance : lookDirection == LookDirection.Right ? camDistance : 0f),
                    playerPos.y,
                    playerPos.z + (lookDirection == LookDirection.Forward ? -camDistance : lookDirection == LookDirection.Backward ? camDistance : 0f)
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

        void ICameraState.ExitState(CameraManager camera) { }
    }
}
