using UnityEngine;

public class Axe : Weapon
{
    // Update is called once per frame
    public override void Move()
    {
        transform.position += direction.normalized * Time.deltaTime * WeaponData.speed;
        transform.Rotate(Vector3.forward, 360*Time.deltaTime);
    }

    public override void Hit(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().enemyHP -= WeaponData.damage;
        }
    }

    public override void LevelUp()
    {
        if (WeaponData.interval > 0.051f) WeaponData.interval -= 0.05f;
        else WeaponData.interval = 0.001f;
    }

    public override void Initiate(Quaternion playerRotation)
    {
        transform.rotation = playerRotation;
        transform.Rotate(new Vector3(1, 0, 0), 90);
        direction = transform.up;
        moveFlg = true;
    }
}
