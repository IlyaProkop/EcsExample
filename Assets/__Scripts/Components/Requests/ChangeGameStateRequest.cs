using Leopotam.Ecs;

namespace Client
{
    internal struct ChangeGameStateRequest
    {
        public GameState NewGameState;
    }

    public enum GameState
    {
        None = 0,
        Before,
        Playing,
        Win,
        Lose,
        Pause
    }
}