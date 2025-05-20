using UnityEngine;

class GameOverState : IState
{
    private GameStateManager gameStateManager;

    public GameOverState(GameStateManager manager)
    {
        gameStateManager = manager;
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
    }

    public void UpdateState() { }

    public void Exit() { }
}
