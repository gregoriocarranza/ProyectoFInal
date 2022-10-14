using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBottleBehaviour : MonoBehaviour
{
    [SerializeField] GameObject destroyedVersion;
    private int life = 1;

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
            Destroy(gameObject);
            Destroy(destroyobj, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
    }
}
