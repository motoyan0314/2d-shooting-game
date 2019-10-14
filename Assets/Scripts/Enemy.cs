using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public int point = 100;
    SpaceShip spaceShip;

    IEnumerator Start()
    {
        spaceShip = GetComponent<SpaceShip>();
        Move(transform.up * -1);

        if (spaceShip.canShoot == false)
        {
            yield break;
        }

        while (true)
        {
            for (int i=0; i<transform.childCount; i++)
            {
                Transform shotPosition = transform.GetChild(i);
                spaceShip.Shot(shotPosition);
            }

            yield return new WaitForSeconds(spaceShip.shotDelay);
        }
    }

    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceShip.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName != "Bullet(Player)") return;

        Transform playerBulletTransform = collision.transform.parent;
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();
        hp = hp - bullet.power;

        Destroy(collision.gameObject);

        if (hp <= 0)
        {
            FindObjectOfType<Score>().AddPoint(point);
            spaceShip.Explosion();
            Destroy(gameObject);
        } else
        {
            spaceShip.GetAnimator().SetTrigger("Damage");
        }
    }
}
