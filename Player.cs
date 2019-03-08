using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    // public or private identify
    //data types ( int, floats, bool, strings )
    //every variable has a NAME
    //option value assigned

    [SerializeField]
    private GameObject _explosionPrefab;

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldAcctive = false;
    public int lives = 3;

  [SerializeField]
  public GameObject _laserPrefab;

  [SerializeField]
  private GameObject _shieldGameObject;

  [SerializeField]
  private GameObject _tripleShotPrefab;

  [SerializeField]
  public float SpeedBoostPower = 10.0f;

  [SerializeField]
  public float _fireRate = 0.25f;

  private float _canFire = 0.0f;

  [SerializeField]
  public float _speed = 5.0f;
    
  
   private void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        //if space key pressed 
        //spawn laser at player position

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot(); 
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
           
        if (canTripleShot == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);       
        }
        else
            {
              Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.896f, 0), Quaternion.identity);
            }

            _canFire = Time.time + _fireRate;
        }

    }

    private void Movement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");    

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed  * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed  * verticalInput * Time.deltaTime);
        }
        //new Vector3(1, 0, 0) * 5 * -1            //(left to right = a, d)
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

        // (up and down = w, s)
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        //if player on the y is greater than 0
        // set player position o 0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //if player on the x >= 8
        // player x pos = 8
        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        
        if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        

    }


    public void Damage()
    {

        if (shieldAcctive == true)
        {
            shieldAcctive = false;
            _shieldGameObject.SetActive(false);
            return;
        }


        lives--;
        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRountine());
    }
    public IEnumerator TripleShotPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRountine());
    }
    public IEnumerator SpeedBoostPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    public void ShieldPowerupOn()
    {
        shieldAcctive = true;
        _shieldGameObject.SetActive(true);
     
    }
    public IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        shieldAcctive = false;
    }
}
