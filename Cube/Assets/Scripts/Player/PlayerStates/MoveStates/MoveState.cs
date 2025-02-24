using UnityEngine;

namespace CubeGame.Player
{
    internal sealed class MoveState : MoveBaseClass
    {
        internal MoveState(Vector2 moveVector) : base(moveVector) {}

        // I will add to some data containers (like Scriptable objects) instead of that
        // But today I am so lazy for it :D

        protected override float _rotation { get; set; } = 90f;  
        protected override float _moveVectorYOffset { get; set; } = -1f;
        protected override float _targetPositonYOffset { get; set; } = 0f;
        
    }
}
