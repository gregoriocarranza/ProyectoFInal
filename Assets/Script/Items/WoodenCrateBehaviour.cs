using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrateBehaviour : MonoBehaviour
{
    [SerializeField] GameObject destroyedVersion;
    [SerializeField] GameObject SpawnItem;
    private int life = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {

            GameObject destroyobj = Instantiate(destroyedVersion, transform.position, transform.rotation);
            if (SpawnItem) Instantiate(SpawnItem, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(destroyobj, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
    }
}
