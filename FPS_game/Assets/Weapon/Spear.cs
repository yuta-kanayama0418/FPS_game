using UnityEngine;

public class Spear : Weapon
{
    public override void Move()
    {
        transform.position += transform.up.normalized * Time.deltaTime * WeaponData.speed;
    }

    public override void Hit(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            collision.GetComponent<Enemy>().enemyHP -= WeaponData.damage;
        }
    }

    public override void LevelUp()
    {
        WeaponData.damage += 10.0f;
        if (WeaponData.interval > 0.051f) WeaponData.interval -= 0.05f;
        else WeaponData.interval = 0.001f;
    }

    public override void Initiate(Quaternion playerRotation)
    {
        transform.rotation = playerRotation;
        transform.Rotate(new Vector3(1, 0, 0), 90);
        moveFlg = true;
    }
}
