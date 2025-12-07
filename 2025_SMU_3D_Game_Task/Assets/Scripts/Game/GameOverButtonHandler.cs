using UnityEngine;

public class GameOverButtonHandler : MonoBehaviour
{
    public void OnClickRetry()
    {
        GameManager.Instance.StartGame();
    }

    public void OnClickExit()
    {
        GameManager.Instance.Exit();
    }
}
