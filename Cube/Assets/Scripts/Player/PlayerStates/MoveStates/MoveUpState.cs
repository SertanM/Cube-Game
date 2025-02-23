using UnityEngine;

namespace CubeGame.Player
{
    internal sealed class MoveUpState : MoveBaseClass
    {
        public MoveUpState(Vector2 moveVector) : base(moveVector) {}

        protected override float _rotation { get; set; } = 180f;
        protected override float _moveVectorYOffset { get; set; } = 1f;
        protected override float _targetPositonYOffset { get; set; } = 1f;
    }
}
