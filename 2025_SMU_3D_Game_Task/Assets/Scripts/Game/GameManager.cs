using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    StartMenu,
    InGame,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public int stageProgress = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetState(GameState.StartMenu);
    }

    public void SetState(GameState state)
    {
        CurrentState = state;

        switch(state)
        {
            case GameState.StartMenu:
                Debug.Log("[GameManager] SGameManager");
                break;

            case GameState.InGame:
                Debug.Log("[GameManager] InGame");
                break;

            case GameState.GameOver:
                Debug.Log("[GameManager] GameOver");
                break;
        }
    }
    public void StartGame()
    {
        stageProgress = 0;
        PlayerStats.Instance.ResetStats();
        SetState(GameState.InGame);
        SceneTransitionManager.Instance.ChangeScene("GameVillageScene");
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneTransitionManager.Instance.ChangeScene("GameOverScene");
    }

    public void ReturnToMenu()
    {
        SetState(GameState.StartMenu);
        SceneTransitionManager.Instance.ChangeScene("GameMenuScene");
    }

    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
