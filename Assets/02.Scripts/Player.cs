using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;

    private int weaponIndex = 0;

    [SerializeField] 
    private Transform shootTransform;

    [SerializeField] 
    private float shootInterval = 0.05f;

    private float lastShotTime = 0f;
    void Update()
    {
        // 키보드로 이동
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.position -= moveTo;
        // }
        // else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        // {
        //     transform.position += moveTo;
        // }
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade()
    {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
}
