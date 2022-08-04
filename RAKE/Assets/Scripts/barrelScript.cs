using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelScript : MonoBehaviour
{

    Collider2D[] inRadius = null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;
    [SerializeField] AudioSource barrelAudio;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(explode());
    }

    IEnumerator explode() 
    {
        yield return new WaitForSeconds(2);
        barrelAudio.pitch = Random.Range(1f, 1.1f);
        barrelAudio.Play();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Explode();
        GetComponent<Animator>().SetBool("Explode", true);
	}

    public void DestroyThis() 
    {
        Destroy(gameObject);
    }

    void Explode() 
    {
        inRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inRadius) 
        {
            Rigidbody2D o_rb = o.GetComponent<Rigidbody2D>();
            if (o_rb != null) 
            {
                Vector2 distance = o.transform.position - transform.position;

                if (o.tag == "Player")
                {
                    o.GetComponent<PlayerMovement>().fallen = true;



                    if (distance.magnitude > 0)
                    {
                        float explosionForce = explosionForceMulti;
                        o_rb.AddForce(distance * explosionForce);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
