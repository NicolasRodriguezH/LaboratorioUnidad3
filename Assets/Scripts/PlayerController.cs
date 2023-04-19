using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 500.0f;
    // Limite para el eje Z
    private float zBound = 4.5f;
    // Si no usa un RigidBody el jugador no es necesario asignarle lo siguiente
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        // En el metodo Start
        // Uso el rigigbody y GetComponent para obtener el componente RB del jugador
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Llamado a los metodos creados anteriormente
        MovePlayer();
        ConstrainPlayerPosition();
    }

    // Move Player - moves player, base on arrow key input
    void MovePlayer()
    {
        // Manejo al Player con las flechas u arrows
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // A continuacion agregamos fuerzas, para poder manejar nuestro objeto o player
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    // Prevent the player from leving the top of botton of the screen
    // Evite que el jugador levante la parte superior o inferior de la pantalla
    void ConstrainPlayerPosition()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Has chocado con un enemigo");
        }
    }

    // En este caso para el potenciador usamos OnTriggerEnter para identificar ese potenciador
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
