using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages bullet behaviour
/// </summary>
public class Bullet : MonoBehaviour {
    public float selfDestructTime = 2;
    private float endTime;
    public GameObject explosion;
    [SerializeField]
    string enemyTag = "Player";

	// Use this for initialization
	void Start () {
        endTime = Time.time + selfDestructTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<FocusCamera>())
        {
            if (Time.time >= endTime && this.GetComponent<FocusCamera>().checkCoroutines())
                Destroy(this.gameObject);
        }
        else
        {
            if (Time.time >= endTime)
                Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Handles collisions with enemies(player or others)
    /// </summary>
    /// <param name="target"></param>
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == enemyTag)
        {
            if (target.gameObject.GetComponent<CharacterController2D>())
            {
                target.gameObject.GetComponent<CharacterController2D>().FallDeath();
            }
            if (target.gameObject.GetComponent<Enemy>())
            {
                target.gameObject.GetComponent<Enemy>().Stunned();
            }
            if (target.gameObject.GetComponent<NewEnemy>())
            {
                target.gameObject.GetComponent<NewEnemy>().Stunned();
            }
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        if(target.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
}
