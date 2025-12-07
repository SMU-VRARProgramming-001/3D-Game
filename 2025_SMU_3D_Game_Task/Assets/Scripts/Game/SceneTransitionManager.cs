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

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
