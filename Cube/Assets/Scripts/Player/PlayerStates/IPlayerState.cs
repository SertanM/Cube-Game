namespace CubeGame.Player
{
    internal interface IPlayerState
    {
        internal void EnterState(PlayerManager player);
        internal void ExitState(PlayerManager player);
        internal void UpdateState(PlayerManager player);
    }
}
