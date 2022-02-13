using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    internal bool moveFlg = false;
    //internal float damage;
    //public static float interval;
    public abstract void Move();
    public abstract void Hit(Collider collision);
    public abstract void LevelUp();
    public abstract void Initiate(Quaternion playerRotation);
    //internal float weaponSpeed;
    internal Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        if (moveFlg)
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Area"))
        {
            Destroy(this.gameObject);
        }
        Hit(collision);
    }

}
