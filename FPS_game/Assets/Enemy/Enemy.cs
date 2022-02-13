using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemySpeed = 4.0f;
    private GameObject playerObject;
    private CharacterController controller;
    private Rigidbody rb;
    public float enemyHP = 30.0f;
    public float enemyDamage = 100.0f;
    private float enemyExp = 10.0f;
    private float damageInterval = 1.0f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            Player.playerExp += enemyExp;
            Destroy(this.gameObject);
        }
        this.transform.LookAt(playerObject.transform);
        rb.position += this.transform.forward.normalized * enemySpeed * Time.deltaTime;
        time += Time.deltaTime;

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && time > damageInterval)
        {
            Player.playerHP -= enemyDamage;
            time = 0.0f;
        }
    }

}
