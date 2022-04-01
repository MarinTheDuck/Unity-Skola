using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    private Rigidbody rigidBodyComponent;
    bool jump;
    bool touchGround;
    private float horizontal;
    private int coins;
    float timer = 0.0f;

    [SerializeField]
    int jmpPwr = 6;

    [SerializeField]
    float mvmntSpd = 1.2f;

    
    void Start() // Start is called before the first frame update
    {
        coins = 0;
        rigidBodyComponent = GetComponent<Rigidbody>();
        jump = false;
        
    }

    
    void Update() // Update is called once per frame
    {
        if(Input.GetKeyDown(KeyCode.Space) && touchGround == true)
        {
            jump = true;
        }
        horizontal = Input.GetAxis("Horizontal") * mvmntSpd;

        timer += Time.deltaTime;

        var f = GameObject.Find("Time").GetComponent<Text>();
        f.text = $"Time: {(int)(timer % 60)}";
    }

    private void FixedUpdate()
    {
        if(jump == true)
        {
            jump = false;
            touchGround = false;
            rigidBodyComponent.AddForce(jmpPwr*Vector3.up, ForceMode.VelocityChange);
        }
        rigidBodyComponent.velocity = new Vector3(horizontal, rigidBodyComponent.velocity.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchGround = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        coins++;
        Destroy(other.gameObject);
        GetComponent<AudioSource>().Play();
        var f = GameObject.Find("Text").GetComponent<Text>();
        f.text = $"Coins: {coins}";
    }

} // .class
