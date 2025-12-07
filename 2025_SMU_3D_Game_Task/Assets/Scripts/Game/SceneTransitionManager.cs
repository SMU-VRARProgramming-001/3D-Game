using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

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
        Debug.Log("Scene Loaded!");
        animator.SetTrigger("Open");
    }
}
