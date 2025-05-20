using UnityEngine;

class GameOverState : IState
{
    private GameStateManager gameStateManager;
    private float timeElapsed = 0f;
    private const float TIMER = 2f;

    public GameOverState(GameStateManager manager)
    {
        gameStateManager = manager;
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
    }

    public void UpdateState()
    {
        timeElapsed += Time.unscaledDeltaTime;

        if (timeElapsed >= TIMER)
        {
            gameStateManager.changeState(GameState.Prepare);
        }
    }

    public void Exit() { }
}
