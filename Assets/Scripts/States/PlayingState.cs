using UnityEngine;

public class PlayingState : IState
{
    private GameStateManager gameStateManager;

    public PlayingState(GameStateManager manager)
    {
        gameStateManager = manager;
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = true;
    }

    public void UpdateState()
    {
        if (gameStateManager.wasHit)
        {
            gameStateManager.changeState(GameState.Pause);
        }
    }

    public void Exit()
    {
        gameStateManager.canFireBullet = false;
        gameStateManager.wasHit = false;
    }
}
