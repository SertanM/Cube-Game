using UnityEngine;
using DG.Tweening;

namespace CubeGame.Player
{
    internal abstract class MoveBaseClass : IPlayerState
    {
        private readonly Vector3 _moveVector;
        private readonly Vector3 _targetQuaternion;
        private Vector3 _targetPosition;
        protected abstract float _rotation { get; set; }
        protected abstract float _moveVectorYOffset { get; set; }
        protected abstract float _targetPositonYOffset { get; set; }
        
        internal MoveBaseClass(Vector2 moveVector)
        {
            _moveVector = new Vector3(moveVector.x, _moveVectorYOffset, moveVector.y);
            _targetQuaternion = new Vector3(_rotation * moveVector.y, 0f, -_rotation * moveVector.x);
        }


        void IPlayerState.EnterState(PlayerManager player)
        {
            MovePlayer(player);
        }


        private void MovePlayer(PlayerManager player)
        {
            _targetPosition = new Vector3(_moveVector.x, _targetPositonYOffset, _moveVector.z) + player.transform.position;

            Vector3 pivotPoint = player.transform.position + (_moveVector / 2f);
            GameObject pivotObject = new GameObject();
            pivotObject.transform.position = pivotPoint;

            player.transform.SetParent(pivotObject.transform);

            pivotObject.transform.DORotate(_targetQuaternion, player.playerSettings.speed * (_rotation / 90f))
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    player.transform.SetParent(null);
                    GameObject.Destroy(pivotObject);


                    // Its just for some fixing
                    player.transform.position = _targetPosition;
                    player.transform.rotation = Quaternion.identity;
                    player.transform.localScale = Vector3.one;

                    player.ChangeState(new IdleState());
                });
        }

        void IPlayerState.UpdateState(PlayerManager player) { }

        void IPlayerState.ExitState(PlayerManager player) { }
    }
}
