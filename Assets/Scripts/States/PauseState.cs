using TMPro;
using UnityEngine;

class PauseState : IState
{
    private GameStateManager gameStateManager;

    private GameObject player1;
    private GameObject player2;

    private float gameOverTimeElapsed = 0f;

    private float timeElapsed = 0f;
    private const float TIMER = 4f;

    private TextMeshProUGUI winnerText;

    private bool fadingOut = false;

    public PauseState(GameStateManager manager)
    {
        gameStateManager = manager;
        player1 = GameObject.Find("LifePoints1");
        player2 = GameObject.Find("LifePoints2");

        winnerText = GameObject.Find("Winner Text").GetComponent<TextMeshProUGUI>();
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
        fadingOut = false;
        timeElapsed = 0f;
        gameOverTimeElapsed = 0f;
    }

    public void UpdateState()
    {
        if (
            player1.GetComponent<LifePoints>().lifePoints == 0
            || player2.GetComponent<LifePoints>().lifePoints == 0
        )
        {
            if (gameOverTimeElapsed < TIMER)
            {
                gameOverTimeElapsed += Time.unscaledDeltaTime;
                if (!fadingOut)
                {
                    gameStateManager.powerUpManager.HandlePowerUpCollected();

                    gameStateManager.player1.GetComponent<PlayerPowerUp>()?.RemoveAllPowerUp();
                    gameStateManager.player2.GetComponent<PlayerPowerUp>()?.RemoveAllPowerUp();
                    gameStateManager.audioManager.FadeOut(
                        "duel",
                        1f,
                        () => {
                            // gameStateManager.audioManager.playSound("win");
                            // gameStateManager.audioManager.FadeIn("win", 4f, 0.2f, () => { });
                        }
                    );
                    fadingOut = true;
                }
                return;
            }

            gameStateManager.changeState(GameState.GameOver);
            string winner =
                (player1.GetComponent<LifePoints>().lifePoints == 0) ? "Green" : "Purple";

            winnerText.text = winner + " Player wins!";

            Debug.Log("Winner variable value: " + gameStateManager.winner);
            return;
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
