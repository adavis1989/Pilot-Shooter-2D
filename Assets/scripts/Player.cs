using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private float _SpeedMultiplier = 2;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private GameObject _TripleShot;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _CanFire = -1f;
    [SerializeField]
    private int _Lives = 3;
    private spawnmanager _spawnManager;


    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _ShieldVisual;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<spawnmanager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager Is NULL.");
        }
        

    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _CanFire)
        {
            ShootLaser();
        }

    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, VerticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void ShootLaser()
    {
        _CanFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(_TripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_LaserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        


    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _ShieldVisual.SetActive(false);
            return;
        }
        
        //if shields is active
        //do nothing
        //deactivate shields
        //return
        _Lives = _Lives - 1;

        if(_Lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotCooldown());
    }


    IEnumerator TripleShotCooldown()
    {
            yield return new WaitForSecondsRealtime(5.0f);
            _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        StartCoroutine(SpeedBoostCooldown());
        _speed *= _SpeedMultiplier;
    }
    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        _speed /= _SpeedMultiplier;
    }
    public void ShieldsActive()
    {
        _isShieldActive = true;
        _ShieldVisual.SetActive(true);
    }
}

