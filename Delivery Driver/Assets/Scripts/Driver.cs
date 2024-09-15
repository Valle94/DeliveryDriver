using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{
    //Starting Variables
    [SerializeField] float steerSpeed = 600f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float emptySpeed = 0.2f;
    [SerializeField] float fuel = 100f;
    [SerializeField] float fuelBurnRate = 20f;
    [SerializeField] Slider fuelSlider;

    //Boost/Slow timer variable
    float timer = 0;

    //Current fuel
    float currentFuel;

    // Start is called before the first frame update
    void Start()
    {
        currentFuel = fuel;
    }

    //slow trigger
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Obstacle")
        {
            moveSpeed = slowSpeed;
            //Slow down for this amount of seconds
            timer = 2;
        }
    }

    //Boost trigger
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Boost")
        {
            //Optional destroy boost on pickup:
            //Destroy(other.gameObject);
            moveSpeed = boostSpeed;
            //Boost for this amount of seconds
            timer = 3;
        }
        if (other.tag == "Fuel")
        {
            currentFuel += 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);

        //boost/slow timer iteration
        timer -= Time.deltaTime;

        //When boost timer is up, return speed to normal
        if (timer <=0)
        {
            moveSpeed = 20f;
        }

        //Decrease fuel if moving
        if (Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0)
        {
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }

        //Slow to a crawl if fuel is empty
        if (currentFuel <= 0)
        {
            currentFuel = 0;
        }
        if (currentFuel == 0)
        {
            moveSpeed = moveSpeed * emptySpeed;
        }
        //Adjust fuel slider based on current fuel
        fuelSlider.value = currentFuel / fuel;
    }
}
