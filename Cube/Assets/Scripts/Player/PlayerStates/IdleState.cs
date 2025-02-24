using System;
using System.Collections.Generic;
using UnityEngine;

namespace CubeGame.Player
{
    internal sealed class IdleState : IPlayerState
    {
        void IPlayerState.EnterState(PlayerManager player)
        {
            SetUpLockPos(player);
            SetLockedPos(player);
            CheckIsWillDrop(player);
            ChecKIsWillTeleport(player);
        }

        private void SetLockedPos(PlayerManager player)
        {
            float nearestValue = player.gameManager.CheckNearestBlockPos(player.transform.position);

            if (nearestValue != Mathf.Infinity && nearestValue != -Mathf.Infinity)
                player.gameManager.lockPos = LockPos.None;
        }

        private static void SetUpLockPos(PlayerManager player)
        {
            LookDirection lookDirection = player.gameManager.lookDirection;

            player.gameManager.lockPos =
                        lookDirection == LookDirection.Forward
                     || lookDirection == LookDirection.Backward
                     ? LockPos.Z : LockPos.X;
        }

        private static void CheckIsWillDrop(PlayerManager player)
        {
            Vector3 targetPos = player.transform.position + Vector3.down;
            if (!player.gameManager.CheckIsThereABlock(targetPos))
                player.ChangeState(new DropState());
        }

        private static void ChecKIsWillTeleport(PlayerManager player)
        {
            Vector3 targetPos = player.transform.position + Vector3.down;

            float nearestValue = player.gameManager.CheckNearestBlockPos(targetPos);

            LockPos lockPos = player.gameManager.lockPos;

            if (nearestValue == Mathf.Infinity || nearestValue == -Mathf.Infinity)
                nearestValue = lockPos == LockPos.Z ? player.transform.position.z : player.transform.position.x;
            
            
            Vector3 playerPos = player.transform.position;
            
            Vector3 newPos = 
                new Vector3(
                    lockPos == LockPos.X ? nearestValue : playerPos.x, 
                    playerPos.y,
                    lockPos == LockPos.Z ? nearestValue : playerPos.z
                );

            player.transform.position = newPos;
        }

        void IPlayerState.UpdateState(PlayerManager player) =>  CheckIsWillMove(player);
        

        private static void CheckIsWillMove(PlayerManager player)
        {
            float moveVector = player.inputManager.GetMovement();

            switch (moveVector)
            {
                case  1f:
                case -1f:
                    ChangeMoveState(player, EditMoveVector(player.gameManager.lookDirection, Vector2.right * moveVector));
                    break;
            }
        }

        private static Vector2 EditMoveVector(LookDirection lookDirection, Vector2 moveVector)
        {
            switch (lookDirection) 
            {
                case LookDirection.Forward:
                    return moveVector;
                case LookDirection.Right:
                    return new Vector2(0f, moveVector.x);
                case LookDirection.Backward:
                    return -moveVector;
                case LookDirection.Left:
                    return new Vector2(0f, -moveVector.x);
                default:
                    throw new Exception($"Invalid look direction: {lookDirection}");
            }
        }

        private static void ChangeMoveState(PlayerManager player, Vector2 moveVector)
        {
            Vector3 targetPos = player.transform.position + new Vector3(moveVector.x, 0f, moveVector.y);

            if (!player.gameManager.CheckIsThereABlock(targetPos))
            {
                player.ChangeState(new MoveState(moveVector));
                return;
            }

            targetPos.y += 1f;

            if (!player.gameManager.CheckIsThereABlock(targetPos))
            {
                player.ChangeState(new MoveUpState(moveVector));
                return;
            }
        }

        void IPlayerState.ExitState(PlayerManager player) { }
    }
}
