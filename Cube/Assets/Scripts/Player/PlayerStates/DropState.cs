using DG.Tweening;

namespace CubeGame.Player
{
    internal sealed class DropState : IPlayerState
    {
        void IPlayerState.EnterState(PlayerManager player) =>  SetDrop(player);
        

        private static void SetDrop(PlayerManager player)
        {
            float targetYPos = player.transform.position.y - 1;
            player.transform.DOMoveY(targetYPos, player.playerSettings.gravitySpeed).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    player.ChangeState(new IdleState());
                });
        }

        void IPlayerState.UpdateState(PlayerManager player) { }

        void IPlayerState.ExitState(PlayerManager player) { }
    }
}
