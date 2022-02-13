using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;
    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 5.0f)
        {
            float x1 = Random.Range(0.0f, 10.0f);
            float z1 = Random.Range(0.0f, 100.0f);
            Instantiate(enemyObject, new Vector3(x1, 1, z1), enemyObject.transform.rotation);
            float x2 = Random.Range(90.0f, 100.0f);
            float z2 = Random.Range(0.0f, 100.0f);
            Instantiate(enemyObject, new Vector3(x2, 1, z2), enemyObject.transform.rotation);
            float x3 = Random.Range(0.0f, 100.0f);
            float z3 = Random.Range(0.0f, 10.0f);
            Instantiate(enemyObject, new Vector3(x3, 1, z3), enemyObject.transform.rotation);
            float x4 = Random.Range(0.0f, 100.0f);
            float z4 = Random.Range(0.0f, 10.0f);
            Instantiate(enemyObject, new Vector3(x4, 1, z4), enemyObject.transform.rotation);
            time = 0.0f;
        }
    }
}
