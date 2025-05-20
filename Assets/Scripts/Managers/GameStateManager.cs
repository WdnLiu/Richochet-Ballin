using System.Collections;
using System.Collections.Generic;
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

    private GameObject player1;
    private GameObject player2;

    private PrepareState prepareState;
    private PlayingState playingState;
    private PauseState pauseState;
    private GameOverState gameOverState;

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

        Debug.Log("Current State: " + gameState.GetType().Name);
    }

    public void changeState(GameState newState)
    {
        gameState.Exit();
        gameState = stateMap[newState];
        gameState.Enter();
    }
}
