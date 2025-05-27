using TMPro;
using UnityEngine;

class PauseState : IState
{
    private GameStateManager gameStateManager;

    private GameObject player1;
    private GameObject player2;

    private float timeElapsed = 0f;
    private const float TIMER = 4f;

    private TextMeshProUGUI winnerText;

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
            string winner = (player1.GetComponent<LifePoints>().lifePoints == 0) ? "Green" : "Blue";

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
