using System.Collections.Generic;
using UnityEngine;

namespace CubeGame
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal List<Vector3> positions = new List<Vector3>();
        internal LockPos lockPos = LockPos.Z;
        internal LookDirection lookDirection = LookDirection.Forward;
    }
}