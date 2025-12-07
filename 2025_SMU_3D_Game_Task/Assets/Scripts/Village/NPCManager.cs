using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCManager : MonoBehaviour
{
    private int statPoint = 0;
    [SerializeField] private GameObject UIPannel;
    [SerializeField] private TMP_Text statPointTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIPannel.SetActive(false);
        statPoint = PlayerStats.Instance.statPoint;
        statPointTxt.text = statPoint.ToString();
    }
    public void GrowHealth()
    {
        if(statPoint > 0)
        {
            PlayerStats.Instance.AddMaxHealthStat(10);
            statPoint--;
            statPointTxt.text = statPoint.ToString();
        }
    }
    public void GrowAttackPower()
    {
        if (statPoint > 0)
        {
            PlayerStats.Instance.AddAttackStat(2);
            statPoint--;
            statPointTxt.text = statPoint.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPannel.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPannel.SetActive(false);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }
}
