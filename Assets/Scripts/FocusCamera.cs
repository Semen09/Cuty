using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour {
    [SerializeField]
    Camera cam;
    [SerializeField]
    float coeff = 0.4f;
    Vector3 currentVelocity;
    public string targetTag = "Enemy";
    bool isReady = true;

	// Use this for initialization
	void Start () {
        if (cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            StartCoroutine("Focus");
        }
    }


    IEnumerator Focus()
    {
        isReady = false;
        Vector3 newPos = Vector3.SmoothDamp(cam.transform.position,new Vector3(transform.position.x, transform.position.y, cam.transform.position.z),ref currentVelocity, 0.01f);

        cam.transform.position = newPos;
        //camera.transform.position = this.transform.position;
        /*for (float f = 0f; f < 20; f += 1)
        {
            camera.fieldOfView -= coeff;
            yield return new WaitForSeconds(.01f);
        }
        for (float f = 0f; f < 20; f += 1)
        {
            camera.fieldOfView += coeff;
            yield return new WaitForSeconds(.01f);
        }*/
        Time.timeScale = 0.2f;
        for (float f = 0f; f < 14; f += 1)
        {
            cam.fieldOfView -= coeff;
            yield return null;
        }
        for (float f = 0f; f < 14; f += 1)
        {
            cam.fieldOfView += coeff;
            yield return null;
        }
        Time.timeScale = 1f;
        isReady = true;
    }
    public bool checkCoroutines()
    {
        return isReady;
    }
}
