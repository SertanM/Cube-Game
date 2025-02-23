using UnityEngine;

namespace CubeGame.Player
{
    internal sealed class MoveState : MoveBaseClass
    {
        internal MoveState(Vector2 moveVector) : base(moveVector) {}

        protected override float _rotation { get; set; } = 90f; // I will add to some data container instead of this
        protected override float _moveVectorYOffset { get; set; } = -1f;
        protected override float _targetPositonYOffset { get; set; } = 0f;
        
    }
}
