using CubeGame.Player;
using DG.Tweening;
using UnityEngine;

namespace CubeGame.Camera
{
    internal abstract class RotateCameraBaseClass : CameraState
    {
        protected abstract float rotationDegree { get; set; }
        protected abstract int increaseAmount { get; set; }
        void CameraState.EnterState(CameraManager camera)
        {
            RotateCamera(camera);
        }

        private void RotateCamera(CameraManager camera)
        {
            camera.player.ChangeState(new BlankState());
            GameObject pivotObject = new GameObject();
            pivotObject.transform.position = camera.player.transform.position;
            camera.transform.SetParent(pivotObject.transform);

            pivotObject.transform.DORotate(Vector3.up * rotationDegree, camera.player.playerSettings.camRotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                camera.transform.SetParent(null);
                GameObject.Destroy(pivotObject);
                camera.ChangeState(new IdleCameraState());
            });
        }

        void CameraState.UpdateState(CameraManager camera) { }

        void CameraState.ExitState(CameraManager camera)
        {
            SetLookDirection(camera);
            SetCam2IdleState(camera);
        }

        private static void SetCam2IdleState(CameraManager camera)
        {
            camera.transform.localScale = Vector3.one;
            camera.player.ChangeState(new IdleState());
        }

        private void SetLookDirection(CameraManager camera)
        {
            camera.gameManager.lookDirection += increaseAmount;

            FixLookDirection(camera);
        }

        private static void FixLookDirection(CameraManager camera)
        {
            LookDirection lookDirection = camera.gameManager.lookDirection;
            if (lookDirection > LookDirection.Right) camera.gameManager.lookDirection = 0;
            if (lookDirection < 0) camera.gameManager.lookDirection = LookDirection.Right;
        }
    }
}
