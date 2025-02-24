using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CubeGame
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal List<Vector3> positions = new List<Vector3>();
        internal LockPos lockPos = LockPos.Z;
        internal LookDirection lookDirection = LookDirection.Forward;

        internal bool CheckIsThereABlock(Vector3 targetPos)
        {
            foreach (var pos in positions)
                if (targetPos.x == pos.x || lockPos == LockPos.X)
                    if (targetPos.y == pos.y)
                        if (targetPos.z == pos.z || lockPos == LockPos.Z)
                            return true;

            return false;
        }

        internal float CheckNearestBlockPos(Vector3 targetPos)
        {
            bool isSmaller = lookDirection == LookDirection.Forward || lookDirection == LookDirection.Left;
            float nearestPos = isSmaller ? Mathf.Infinity : -Mathf.Infinity;

            foreach (var pos in positions)
                if (targetPos.x == pos.x || lockPos == LockPos.X)
                    if (targetPos.y == pos.y)
                        if (targetPos.z == pos.z || lockPos == LockPos.Z)
                            nearestPos = SetNewNearestPoint(isSmaller, nearestPos, pos);
                        

            return nearestPos;
        }

        private float SetNewNearestPoint(bool isSmaller, float nearestPos, Vector3 pos)
        {
            float newPos = lockPos == LockPos.Z ? pos.z : pos.x;

            if (isSmaller)
                if (newPos < nearestPos)
                    nearestPos = newPos;

            if (!isSmaller)
                if (newPos > nearestPos)
                    nearestPos = newPos;
            return nearestPos;
        }
    }
}