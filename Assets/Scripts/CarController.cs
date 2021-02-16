using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    private bool _canMove = true;

    [SerializeField]
    private float _rotationSpeed = 180f;
    [SerializeField]
    private float _moveSpeed = 0.1f;
    [SerializeField]
    private float _slipSpeed = 1f;

    private void Update()
    {
        if (_canMove == false) return;
        if (transform.position.y < -0.2f)
        {
            StartCoroutine(Restart());
        }

        if (Input.GetMouseButton(0))
        {
            if (transform.rotation.eulerAngles.y < 90f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * Time.deltaTime * _rotationSpeed);
                transform.position += Vector3.forward * _slipSpeed * Time.deltaTime;
            }
        }
        else if (transform.rotation.eulerAngles.y > 0.01f)
        {
            Vector3 temp = transform.rotation.eulerAngles - Vector3.up * Time.deltaTime * _rotationSpeed;
            if (temp.y < 0f)
            {
                temp.y = 0f;
            }

            transform.rotation = Quaternion.Euler(temp);
            transform.position += Vector3.right * _slipSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            transform.position += transform.forward.normalized * _moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            StartCoroutine(Finish());
        }
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.5f);
        _canMove = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private IEnumerator Restart()
    {
        _canMove = false;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
