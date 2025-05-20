using TMPro;
using UnityEngine;

class GameOverState : IState
{
    private GameStateManager gameStateManager;
    private float timeElapsed = 0f;
    private const float TIMER = 10f;

    // private GameObject environment;
    // private TextMeshProUGUI winnerText;

    public GameOverState(GameStateManager manager)
    {
        gameStateManager = manager;
        // environment = GameObject.Find("Environment");
        // winnerText = gameStateManager.winText;
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
        // environment.SetActive(false);

        // winnerText.text = "Player " + gameStateManager.winner + " wins!";

        // winnerText.gameObject.SetActive(true);
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
        // environment.SetActive(true);
        // winnerText.gameObject.SetActive(false);
    }
}
