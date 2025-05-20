using UnityEngine;

class PauseState : IState
{
    private GameStateManager gameStateManager;
    private float timeElapsed = 0f;

    private GameObject player1;
    private GameObject player2;

    public PauseState(GameStateManager manager)
    {
        gameStateManager = manager;
        player1 = GameObject.Find("LifePoints1");
        player2 = GameObject.Find("LifePoints2");
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
    }

    public void UpdateState()
    {
        if (
            player1.GetComponent<LifePoints>().lifePoints == 0
            || player2.GetComponent<LifePoints>().lifePoints == 0
        )
            gameStateManager.changeState(GameState.GameOver);
        else
            timeElapsed += Time.unscaledDeltaTime;

        if (timeElapsed >= 2f)
            gameStateManager.changeState(GameState.Playing);
    }

    public void Exit() { }
}
