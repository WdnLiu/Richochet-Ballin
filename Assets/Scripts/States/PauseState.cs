using UnityEngine;

class PauseState : IState
{
    private GameStateManager gameStateManager;

    public PauseState(GameStateManager manager)
    {
        gameStateManager = manager;
    }

    public void Enter() { }

    public void UpdateState() { }

    public void Exit() { }
}
