using UnityEngine;

public class VillageManager : MonoBehaviour
{
    // Scene Load
    public void LoadStage1()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneTransitionManager.Instance.ChangeScene("GameStage1");
    }
    public void LoadStage2()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneTransitionManager.Instance.ChangeScene("GameStage2");
    }
    public void LoadBossStage()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneTransitionManager.Instance.ChangeScene("GameBossStage");
    }
}
