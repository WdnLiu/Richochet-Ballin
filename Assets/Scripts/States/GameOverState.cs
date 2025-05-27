using UnityEngine;

class GameOverState : IState
{
    private GameStateManager gameStateManager;
    private GameObject environment;
    private float timeElapsed = 0f;
    private const float TIMER = 10f;

    private GameObject winScreen;

    public GameOverState(GameStateManager manager)
    {
        gameStateManager = manager;
        environment = GameObject.Find("Environment");
        winScreen = GameObject.Find("Winning Screen");
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
        timeElapsed = 0f;
        gameStateManager.powerUpManager.HandlePowerUpCollected();

        gameStateManager.player1.GetComponent<PlayerPowerUp>()?.RemoveAllPowerUp();
        gameStateManager.player2.GetComponent<PlayerPowerUp>()?.RemoveAllPowerUp();

        environment.SetActive(false);

        gameStateManager.audioManager.FadeOut(
            "duel",
            1f,
            () =>
            {
                gameStateManager.audioManager.playSound("win");
                gameStateManager.audioManager.FadeIn("win", 2f, 0.5f, () => { });
            }
        );

        winScreen.SetActive(true);
    }

    public void UpdateState()
    {
        timeElapsed += Time.unscaledDeltaTime;

        if (timeElapsed >= TIMER)
        {
            gameStateManager.changeState(GameState.Prepare);
        }
    }

    public void Exit()
    {
        environment.SetActive(true);
        winScreen.SetActive(false);

        gameStateManager.audioManager.FadeOut(
            "win",
            1f,
            () =>
            {
                gameStateManager.audioManager.FadeIn("standoff", 2f, 1f, () => { });
            }
        );
    }
}
