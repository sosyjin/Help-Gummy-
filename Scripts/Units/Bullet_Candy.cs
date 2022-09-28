using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Candy : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryBullet", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * (-1), distance, isLayer);
        if (raycast.collider != null) {
            if(raycast.collider.tag == "Jelly") {
                Debug.Log("Hit");
            }
            DestroyBullet();
        }

        transform.Translate( -1f * speed * Time.deltaTime * transform.right );
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
