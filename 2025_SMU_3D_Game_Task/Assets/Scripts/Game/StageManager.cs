using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int enemiesToKill;
    private int currentKillCount = 0;
    private bool isCleared = false;

    private string townSceneName = "GameVillageScene";
    private void Start()
    {
        currentKillCount = 0;
        isCleared = false;
    }

    public void OnEnemyKilled()
    {
        if (isCleared) return;

        currentKillCount++;
        Debug.Log($"Enemy Killed ({currentKillCount}/{enemiesToKill})");

        if (currentKillCount >= enemiesToKill)
        {
            StageClear();
        }
    }

    private void StageClear()
    {
        isCleared = true;
        Debug.Log("Stage Clear!");

        GameManager.Instance.stageProgress++;
        PlayerStats.Instance.statPoint++;
        StartCoroutine(GoToTownAfterDelay(2f));
    }


    private IEnumerator GoToTownAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneTransitionManager.Instance.ChangeScene(townSceneName);
    }
}
