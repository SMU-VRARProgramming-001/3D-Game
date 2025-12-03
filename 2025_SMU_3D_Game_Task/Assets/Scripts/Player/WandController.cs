using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private ParticleSystem VFX;

    public int attackAmount = 1;

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
            Debug.Log("Hit Enemy: " + hit.collider.name);
            hit.transform.gameObject.GetComponent<EnemyBase>().Damaged(attackAmount);
        }
        else
        {
            Debug.Log("Hit Something Else");
        }
    }
}
