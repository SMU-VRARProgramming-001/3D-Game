using UnityEngine;
using UnityEngine.Audio;

public class StageEntrance : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private int requiredProgress;
    [SerializeField] private GameObject enterPanel;
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dieSFX;

    private void Start()
    {
        enterPanel.SetActive(false);
        enterBtn.SetActive(false);  
    }

    public void OnEnterBtn()
    {
        SceneTransitionManager.Instance.ChangeScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enterPanel.SetActive(true);
            if (GameManager.Instance.stageProgress >= requiredProgress)
            {
                enterBtn.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (audioSource != null && dieSFX != null)
            {
                audioSource.PlayOneShot(dieSFX);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterPanel.SetActive(false);
            enterBtn.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
