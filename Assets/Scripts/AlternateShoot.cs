using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlternateShoot : MonoBehaviour
{
    public Transform shootingPoint;
    public Transform shootingPoint2;
    public GameObject bulletPrefab;
    public float pressedSpace;
    public AudioSource shotgunShootSFX;

    // Start is called before the first frame update
    void Start()
    {
        pressedSpace = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool input = Input.GetKeyDown("space");
        ///bool input2 = Mouse.current.rightButton.wasPressedThisFrame;
        if(Input.GetKeyDown(KeyCode.X) && Time.timeScale > 0)
        {
            shotgunShootSFX.Play();
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            Instantiate(bulletPrefab, shootingPoint2.position, transform.rotation);
        }

        if (input) 
        {
            pressedSpace = 2f;
        }
        if (pressedSpace > 0) pressedSpace -= 0.1f;
    }
}
