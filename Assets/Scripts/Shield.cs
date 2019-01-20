using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    bool shieldEnable = false;
    Transform shield;
    [SerializeField]
    UnityEngine.UI.Slider restBar;

    // Use this for initialization
    void Start()
    {
        shield = transform.Find("Shield").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (GameManager.gm && GameManager.gm.GetShieldPoints() > 0 && !shieldEnable)
            {
                if (transform.Find("Shield"))
                {
                    shield.gameObject.SetActive(true);
                    shield.transform.localPosition = new Vector3(0.341f, -0.180f, 0);
                }
                else
                    print("Error, no shield child");
                shieldEnable = true;
            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (GameManager.gm && GameManager.gm.GetShieldPoints() > 0 && shieldEnable)
            {
                TurnOffShield();
                shieldEnable = false;
            }
        }
        if (shieldEnable)
        {
            GameManager.gm.SetShieldPoints(-Time.deltaTime);
            if(GameManager.gm.GetShieldPoints() <= 0)
            {
                if(restBar.IsActive())
                {
                    restBar.gameObject.SetActive(false);
                }
                TurnOffShield();
            }
            restBar.value = GameManager.gm.GetShieldPoints();
        }
        if(GameManager.gm.GetShieldPoints() > 0 && !restBar.IsActive())
        {
            restBar.gameObject.SetActive(true);
        }
    }
    void TurnOffShield()
    {
        shield.gameObject.SetActive(false);
        shieldEnable = false;
    }
}
