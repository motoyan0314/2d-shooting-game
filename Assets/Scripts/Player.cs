using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpaceShip spaceShip;

    IEnumerator Start() {
        spaceShip = GetComponent<SpaceShip>();
        while (true)
        {
            spaceShip.Shot(transform);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(spaceShip.shotDelay);
        }
    }

    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;
        pos += direction * spaceShip.speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName == "Bullet(Enemy)")
        {
            Destroy(collision.gameObject);
        }

        if (layerName == "Bullet(Enemy)" || layerName == "Enemy")
        {
            FindObjectOfType<Manager>().GameOver();
            spaceShip.Explosion();
            Destroy(gameObject);
        }
    }
}
