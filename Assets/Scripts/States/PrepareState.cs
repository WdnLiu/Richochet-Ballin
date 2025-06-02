using UnityEngine;

public class PrepareState : IState
{
    private GameObject startZone1;
    private GameObject startZone2;
    private GameObject player1;
    private GameObject player2;
    private GameStateManager gameStateManager;
    private float timeElapsed = 0f;
    private GameObject winScreen;
    const float TIMER = 2.8f;
    private bool isStartCondition = false;

    private float startZoneInitTimer = 0f;

    public PrepareState(GameStateManager manager)
    {
        gameStateManager = manager;
        startZone1 = GameObject.Find("StartPoint1");
        startZone2 = GameObject.Find("StartPoint2");
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        winScreen = GameObject.Find("Winning Screen");
    }

    public void Enter()
    {
        gameStateManager.canFireBullet = false;
        timeElapsed = 0f;
        GameObject.Find("LifePoints1").GetComponent<LifePoints>().setLifePoints(3);
        GameObject.Find("LifePoints2").GetComponent<LifePoints>().setLifePoints(3);

        startZone1.GetComponent<StartExperience>().playerTrigger = false;
        startZone2.GetComponent<StartExperience>().playerTrigger = false;

        startZone1.GetComponent<StartExperience>().setDefaultColor();
        startZone2.GetComponent<StartExperience>().setDefaultColor();

        gameStateManager.audioManager.playSound("standoff");

        winScreen.SetActive(false);
        startZone1.SetActive(true);
        startZone2.SetActive(true);
        startZoneInitTimer = 0f;
    }

    public void UpdateState()
    {
        if (timeElapsed >= TIMER)
        {
            gameStateManager.changeState(GameState.Playing);
        }

        startZoneInitTimer += Time.unscaledDeltaTime;

        StartExperience startExperience1 = startZone1.GetComponent<StartExperience>();
        StartExperience startExperience2 = startZone2.GetComponent<StartExperience>();

        if (startExperience1.playerTrigger && startExperience2.playerTrigger)
        {
            Vector3 player1ToCenter = Vector3.zero - player1.transform.position;
            Vector3 player2ToCenter = Vector3.zero - player2.transform.position;

            Vector3 direction1 = player1.transform.forward;
            Vector3 direction2 = player2.transform.forward;

            bool isFacingOut =
                Vector3.Dot(player1ToCenter, direction1) < 0
                && Vector3.Dot(player2ToCenter, direction2) < 0;

            bool isFacingAgainst = Vector3.Dot(direction1, direction2) < 0;

            if (isFacingOut && isFacingAgainst && startZoneInitTimer > 4f)
            {
                if (!isStartCondition)
                {
                    isStartCondition = true;
                    gameStateManager.audioManager.playSound("countdown");
                }
                timeElapsed += Time.deltaTime;
            }
            else
            {
                isStartCondition = false;
                gameStateManager.audioManager.stopSound("countdown");
                timeElapsed = 0f;
            }
        }
        else
        {
            isStartCondition = false;
            gameStateManager.audioManager.stopSound("countdown");
        }
    }

    public void Exit()
    {
        startZone1.SetActive(false);
        startZone2.SetActive(false);

        gameStateManager.audioManager.FadeOut(
            "standoff",
            1f,
            () =>
            {
                gameStateManager.audioManager.FadeIn("duel", 2f, 0.2f, () => { });
            }
        );
    }
}
