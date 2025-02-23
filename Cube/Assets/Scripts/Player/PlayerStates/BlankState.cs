namespace CubeGame.Player
{
    internal sealed class BlankState : IPlayerState
    {
        void IPlayerState.EnterState(PlayerManager player) { }

        void IPlayerState.ExitState(PlayerManager player) { }

        void IPlayerState.UpdateState(PlayerManager player) { }
    }
}
