using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private float _Speed = 3;
    [SerializeField] //0 = tripleshot 1 = speed 2 = shield
    private int powerupID;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8, 8), 7, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);
        if(transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }    
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();

        if (player != null)
        {
            switch(powerupID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedBoostActive();
                    break;
                case 2:
                    player.ShieldsActive();
                    break;
            }
        }
        Destroy(this.gameObject);
    }



}
