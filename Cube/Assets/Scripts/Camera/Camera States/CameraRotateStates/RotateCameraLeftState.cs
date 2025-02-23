namespace CubeGame.Camera
{
    internal sealed class RotateCameraLeftState : RotateCameraBaseClass
    {
        protected override float rotationDegree { get; set; } = -90f;
        protected override int increaseAmount { get; set; } = -1;
    }
}
