using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 4.5f;
    [SerializeField]
    public float rotationSpeed = 2.0f;
    [SerializeField]
    private float playerMaxHP = 100.0f;
    [SerializeField]
    private float requiredExp = 100;
    [SerializeField]
    public GameObject[] weaponData;

    public static float playerHP;
    public static float playerExp;
    private Slider hpBar;
    private Slider expBar;
    private Text levelUp;
    private CharacterController controller;
    public static int playerWeapon;
    private float attackInterval;
    private float cameraVerticalAngle;

    public static bool onGame = true;

    private enum weaponNum
    {
        Spear,
        Axe
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        hpBar = GameObject.Find("HPbar").GetComponent<Slider>();
        expBar = GameObject.Find("EXPbar").GetComponent<Slider>();
        levelUp = GameObject.Find("Levelup").GetComponent<Text>();
        levelUp.gameObject.SetActive(false);
        playerHP = playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGame)
        {
            Move();
            Attack();
            RotateCamera();
        }
        attackInterval += Time.deltaTime;
        hpBar.value = playerHP / playerMaxHP;
        expBar.value = playerExp / requiredExp;

        if (expBar.value >= 1.0f)
        {
            playerExp = 0.0f;
            requiredExp += 50.0f;
            StartCoroutine("LevelUp");
            switch (playerWeapon)
            {
                case (int)weaponNum.Spear:
                    weaponData[playerWeapon].GetComponent<Spear>().LevelUp();
                    break;
                case (int)weaponNum.Axe:
                    weaponData[playerWeapon].GetComponent<Axe>().LevelUp();
                    break;
            }
        }
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        float moveX = inputX * playerSpeed;
        float moveZ = inputZ * playerSpeed;
        controller.SimpleMove(transform.forward * moveZ + transform.right * moveX);
    }

    private void RotateCamera()
    {
        // horizontal character rotation
        {
            // rotate the transform with the input speed around its local Y axis
            transform.Rotate(new Vector3(0f, (Input.GetAxisRaw("Mouse X") * rotationSpeed), 0f), Space.Self);
        }

        // vertical camera rotation
        {
            // add vertical inputs to the camera's vertical angle
            cameraVerticalAngle += Input.GetAxisRaw("Mouse Y") * rotationSpeed * -1f;

            // limit the camera's vertical angle to min/max
            cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

            // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
            GameObject.FindGameObjectWithTag("MainCamera").transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
        }
    }

    private void Attack()
    {
        if (attackInterval > WeaponData.interval)
        {
            GameObject weapon;
            switch (playerWeapon)
            {
                case (int) weaponNum.Spear:
                    weapon = Instantiate(weaponData[playerWeapon], transform.position,
                        weaponData[playerWeapon].transform.rotation);
                    Spear spearInstance = weapon.GetComponent<Spear>();
                    spearInstance.Initiate(transform.rotation);
                    attackInterval = 0.0f;
                    break;
                case (int) weaponNum.Axe:
                    weapon = Instantiate(weaponData[playerWeapon], transform.position,
                        weaponData[playerWeapon].transform.rotation);
                    Axe axeInstance = weapon.GetComponent<Axe>();
                    axeInstance.Initiate(transform.rotation);
                    attackInterval = 0.0f;
                    break;
            }
        }
    }

    public void SetSpear()
    {
        playerWeapon = (int)weaponNum.Spear;
        WeaponData.damage = 20.0f;
        WeaponData.speed = 100.0f;
        WeaponData.interval = 0.5f;
    }

    public void SetAxe()
    {
        playerWeapon = (int) weaponNum.Axe;
        WeaponData.damage = 50.0f;
        WeaponData.speed = 5.0f;
        WeaponData.interval = 3.0f;
    }

    IEnumerator LevelUp()
    {
        levelUp.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        levelUp.gameObject.SetActive(false);
    }
}
