using System.Data;
using TMPro;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private TMP_Text attackPowerTxt;
    [SerializeField] private ParticleSystem VFX;

    private int attackPower;

    private void OnEnable()
    {
        PlayerStats.Instance.OnStatsChanged += UpdateStat;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.OnStatsChanged -= UpdateStat;
    }

    private void Start()
    {
        UpdateStat();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        VFX.Play();
        if (Physics.Raycast(ray, out hit, maxDistance, 1 << 7))
        {
            //Debug.Log("Hit Enemy: " + hit.collider.name);
            hit.transform.gameObject.GetComponent<EnemyBase>().Damaged(attackPower);
        }
    }
    private void UpdateStat()
    {
        attackPower = PlayerStats.Instance.attackPower;
        attackPowerTxt.text = attackPower.ToString();
    }
}
