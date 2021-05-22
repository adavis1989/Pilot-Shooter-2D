using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(Random.Range(-8, 8), 6, 0);



    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y <= -6)
        {
            transform.position = new Vector3(Random.Range(-11f, 11), 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        
        Player player = other.transform.GetComponent<Player>();
        
        if(player != null)
        {
            player.Damage();
        }

        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }








    }

    



}
