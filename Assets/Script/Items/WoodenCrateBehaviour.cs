using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCrateBehaviour : MonoBehaviour
{
    [SerializeField] GameObject destroyedVersion;
    private int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0){
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        life -= damage;
    }
}
