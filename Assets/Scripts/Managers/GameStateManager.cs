using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    Prepare,
    Playing,
    Pause,
    GameOver,
}

public class GameStateManager : MonoBehaviour
{
    public bool canFireBullet = false;
    public bool wasHit = false;
    public bool isPaused = false;
    public int winner = 0;

    [Header("UI References")]
    public TextMeshProUGUI winText;

    public GameObject player1;
    public GameObject player2;

    private PrepareState prepareState;
    private PlayingState playingState;
    private PauseState pauseState;
    private GameOverState gameOverState;

    public PowerUpLogic powerUpManager;

    private Dictionary<GameState, IState> stateMap = new Dictionary<GameState, IState>();

    private IState gameState;

    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        prepareState = new PrepareState(this);
        playingState = new PlayingState(this);
        pauseState = new PauseState(this);
        gameOverState = new GameOverState(this);
        powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpLogic>();

        stateMap[GameState.Prepare] = prepareState;
        stateMap[GameState.Playing] = playingState;
        stateMap[GameState.Pause] = pauseState;
        stateMap[GameState.GameOver] = gameOverState;

        gameState = prepareState;
        gameState.Enter();
    }

    void Update()
    {
        gameState.UpdateState();
    }

    public void changeState(GameState newState)
    {
        gameState.Exit();
        gameState = stateMap[newState];
        gameState.Enter();
    }
}
