using UnityEngine;

class PauseState : IState
{
    private GameStateManager gameStateManager;

    private GameObject player1;
    private GameObject player2;

    private float timeElapsed = 0f;
    private const float TIMER = 4f;

    public PauseState(GameStateManager manager)
    {
        gameStateManager = manager;
        player1 = GameObject.Find("LifePoints1");
        player2 = GameObject.Find("LifePoints2");
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
        timeElapsed = 0f;
    }

    public void UpdateState()
    {
        if (
            player1.GetComponent<LifePoints>().lifePoints == 0
            || player2.GetComponent<LifePoints>().lifePoints == 0
        )
        {
            gameStateManager.changeState(GameState.GameOver);
            gameStateManager.winner = player1.GetComponent<LifePoints>().lifePoints == 0 ? 2 : 1;
            Debug.Log("Winner variable value: " + gameStateManager.winner);
        }

        if (timeElapsed >= TIMER)
        {
            gameStateManager.changeState(GameState.Playing);
        }

        timeElapsed += Time.unscaledDeltaTime;
    }

    public void Exit()
    {
        gameStateManager.wasHit = false;
    }
}
