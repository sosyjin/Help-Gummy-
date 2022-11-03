using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Jelly : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestoryBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * 1, distance, isLayer);
        if (raycast.collider != null) {
            if (raycast.collider.tag == "Candy") {
                CandyUnit candyUnit = raycast.collider.gameObject.GetComponent<CandyUnit>();
                candyUnit.hp -= 3;
            }
            DestroyBullet();
        }

        transform.Translate(transform.right * 1f * speed * Time.deltaTime);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
