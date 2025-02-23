using UnityEngine;
using Zenject;

namespace CubeGame.Blocks
{
    internal sealed class Block : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        private void OnEnable()
            =>  gameManager.positions.Add(transform.position);
    }
}