using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class SpaceShip : MonoBehaviour
{    
    public float speed;
    public float shotDelay;
    public GameObject bullet;
    public bool canShoot;
    public GameObject explosion;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shot(Transform origin)
    {
        Instantiate(bullet, origin.position, origin.rotation);
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
