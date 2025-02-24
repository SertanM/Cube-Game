namespace CubeGame.Camera
{
    internal interface ICameraState
    {
        internal void EnterState(CameraManager camera);
        internal void UpdateState(CameraManager camera);
        internal void ExitState(CameraManager camera);
    }
}
