using UnityEngine;

public class VillageManager : MonoBehaviour
{
    // Scene Load
    public void LoadStage1()
    {
        SceneTransitionManager.Instance.ChangeScene("GameStage1");
    }
    public void LoadStage2()
    {
        SceneTransitionManager.Instance.ChangeScene("GameStage2");
    }
    public void LoadBossStage()
    {
        SceneTransitionManager.Instance.ChangeScene("GameBossStage");
    }
}
