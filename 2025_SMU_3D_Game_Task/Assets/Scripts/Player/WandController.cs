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

        if (VFX != null)
            VFX.Play();

        if (Physics.Raycast(ray, out hit, maxDistance, 1 << 7))
        {
            Debug.Log("Hit Enemy: " + hit.collider.name);
            var enemy = hit.transform.GetComponentInParent<EnemyBase>();
            if (enemy != null)
            {
                enemy.Damaged(attackPower);
            }
        }
        else
        {
            Debug.Log("Miss!");
        }
    }

    /*    private void Shoot()
        {
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2f, Screen.height / 2f)
            );
            RaycastHit hit;

            if (VFX != null)
                VFX.Play();

            // 디버그용: 모든 레이어 다 맞추기
            if (Physics.Raycast(ray, out hit, maxDistance))   // <-- 레이어마스크 제거
            {
                Debug.Log($"Hit: {hit.collider.name}, layer={hit.collider.gameObject.layer}");
                var enemy = hit.collider.GetComponentInParent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.Damaged(attackPower);
                }
            }
            else
            {
                Debug.Log("Miss (no collider at all)");
            }
        }*/


    private void UpdateStat()
    {
        attackPower = PlayerStats.Instance.attackPower;
        attackPowerTxt.text = attackPower.ToString();
    }
}
