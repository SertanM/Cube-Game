namespace CubeGame.Camera
{
    internal sealed class RotateCameraRightState : RotateCameraBaseClass
    {
        protected override float rotationDegree { get; set; } = 90f;
        protected override int increaseAmount { get; set; } = 1;
    }
}
