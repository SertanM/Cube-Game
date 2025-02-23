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
            ChecKIsWillTeleportOnLockedPos(player);
        }

        private void SetLockedPos(PlayerManager player)
        {
            float nearestValue = CheckNearestLockedPoint(player.transform.position, player.gameManager);

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
            if (!CheckIsThereSphere(targetPos, player.gameManager))
                player.ChangeState(new DropState());
        }

        private static void ChecKIsWillTeleportOnLockedPos(PlayerManager player)
        {
            Vector3 targetPos = player.transform.position + Vector3.down;

            float nearestValue = CheckNearestLockedPoint(targetPos, player.gameManager);

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

        void IPlayerState.UpdateState(PlayerManager player)
        {
            CheckIsWillMove(player);
        }

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

            if (!CheckIsThereSphere(targetPos, player.gameManager))
            {
                player.ChangeState(new MoveState(moveVector));
                return;
            }

            targetPos.y += 1f;

            if (!CheckIsThereSphere(targetPos, player.gameManager))
            {
                player.ChangeState(new MoveUpState(moveVector));
                return;
            }
        }


        private static bool CheckIsThereSphereWithoutLockedPos(Vector3 targetPos, List<Vector3> positions)
        {
            foreach (var pos in positions)
                if (targetPos == pos)
                    return true;

            return false;
        }

        private static bool CheckIsThereSphere(Vector3 targetPos, GameManager gameManager)
        {
            LockPos lockPos = gameManager.lockPos;
            List<Vector3> positions = gameManager.positions;

            foreach (var pos in positions)
                if (targetPos.x == pos.x || lockPos == LockPos.X)
                    if (targetPos.y == pos.y)
                        if (targetPos.z == pos.z || lockPos == LockPos.Z)
                            return true;

            return false;
        }

        private static float CheckNearestLockedPoint(Vector3 targetPos, GameManager gameManager)
        {
            LockPos lockPos = gameManager.lockPos;
            LookDirection lookDirection = gameManager.lookDirection;

            bool isSmaller = lookDirection == LookDirection.Forward || lookDirection == LookDirection.Left;
            
            List<Vector3> positions = gameManager.positions;

            float nearestPos = isSmaller ? Mathf.Infinity : -Mathf.Infinity;

            foreach (var pos in positions) 
            {
                if (!(targetPos.x == pos.x || lockPos == LockPos.X))
                    continue;

                if (targetPos.y != pos.y)
                    continue;

                if (!(targetPos.z == pos.z || lockPos == LockPos.Z))
                    continue;

                
                float newPos = lockPos == LockPos.Z ? pos.z : pos.x;

                if (isSmaller)
                    if (newPos < nearestPos)
                        nearestPos = newPos;

                if (!isSmaller)
                    if (newPos > nearestPos)
                        nearestPos = newPos;
            }

            
            return nearestPos;
        }

        void IPlayerState.ExitState(PlayerManager player) { }
    }
}
