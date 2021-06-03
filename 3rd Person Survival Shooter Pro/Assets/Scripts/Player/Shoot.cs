using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodSplatter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);

            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);

                Health targetHP = hitInfo.collider.GetComponentInParent<Health>();

                if (targetHP)
                {
                    GameObject bloodSplat = Instantiate(_bloodSplatter, hitInfo.point, Quaternion.FromToRotation(Vector3.forward, hitInfo.normal));
                    targetHP.Damage(50);
                    StartCoroutine(BloodCleanupRoutine(bloodSplat));
                }
            }

        }
    }

    IEnumerator BloodCleanupRoutine(GameObject blood)
    {
        yield return new WaitForSeconds(2f);
        Destroy(blood);
    }
}
