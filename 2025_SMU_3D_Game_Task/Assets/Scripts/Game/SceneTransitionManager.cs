using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private GameObject playerPrefab;
    private GameObject playerInstance;

    [Header("Animation")]
    public Animator animator;
    private float transitionTime = 1f;

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject circleImg;

    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject stage2;
    [SerializeField] private GameObject bossStage;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;       
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Start()
    {
        playerPrefab = Resources.Load<GameObject>("Player");

        cam.gameObject.SetActive(false);
        stage1.SetActive(false);
        stage2.SetActive(false);
        bossStage.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(DoTransition(sceneName));
    }
    private IEnumerator DoTransition(string sceneName)
    {
        Debug.Log("Scene Loading...");

        animator.SetTrigger("Close");
        yield return new WaitForSeconds(transitionTime);

        GameObject cutscene = GetCutscene(sceneName);
        if (cutscene != null)
        {
            DestroyPlayer();

            SetVillageCanvas(false);
            circleImg.SetActive(false);
            cam.gameObject.SetActive(true);

            cutscene.SetActive(true);
            yield return new WaitForSeconds(4f);
            cutscene.SetActive(false);

            SetVillageCanvas(true);
            circleImg.SetActive(true);
        }

        SceneManager.LoadScene(sceneName);
    }


    private GameObject GetCutscene(string sceneName)
    {
        switch (sceneName)
        {
            case "GameStage1": return stage1;
            case "GameStage2": return stage2;
            case "GameBossStage": return bossStage;
            default: return null;
        }
    }
    private void SetVillageCanvas(bool active)
    {
        GameObject canvas = GameObject.FindWithTag("VillageCanvas");
        if (canvas != null)
        {
            canvas.SetActive(active);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam.gameObject.SetActive(false);

        Transform spawnPoint = GameObject.FindWithTag("PlayerSpawn")?.transform;
        if (spawnPoint != null)
        {
            SpawnPlayer(spawnPoint.position);
        }
        else
        {
            DestroyPlayer();
        }

        animator.SetTrigger("Open");
    }

    private void SpawnPlayer(Vector3 position)
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab, position, Quaternion.identity);
            DontDestroyOnLoad(playerInstance);
        }
        else
        {
            playerInstance.transform.position = position;
        }
    }

    private void DestroyPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
        playerInstance = null;
    }
}
