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
        gameStateManager.powerUpManager.canSpawn = true;
        gameStateManager.winText.gameObject.SetActive(true);
    }

    public void UpdateState()
    {
        if (gameStateManager.wasHit)
        {
            gameStateManager.changeState(GameState.Pause);
            Debug.Log("wasHit: " + gameStateManager.wasHit);
        }
    }

    public void Exit()
    {
        gameStateManager.canFireBullet = false;
        gameStateManager.powerUpManager.canSpawn = false;
    }
}
