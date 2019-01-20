using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {
    bool timeFreezeEnable = false;
    [RangeAttribute(0, 1)]
    [SerializeField]
    float coeff = 0.5f;
    [SerializeField]
    UnityEngine.UI.Slider restBar;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire3"))
        {
            if (GameManager.gm && GameManager.gm.GetFreezeTimePoints() > 0 && !timeFreezeEnable)
            {
                Time.timeScale = coeff;
                this.GetComponent<CharacterController2D>().moveSpeed *= 1f/coeff;
                timeFreezeEnable = true;
            }
        }
        if(Input.GetButtonUp("Fire3"))
        {
            if (GameManager.gm && GameManager.gm.GetFreezeTimePoints() > 0 && timeFreezeEnable)
            {
                TurnOffFreezing();
                timeFreezeEnable = false;
            }
        }
        if (timeFreezeEnable)
        {
            GameManager.gm.SetFreezeTimePoints(-Time.deltaTime);
            if(GameManager.gm.GetFreezeTimePoints() <= 0)
            {
                if (restBar.IsActive())
                {
                    restBar.gameObject.SetActive(false);
                }
                TurnOffFreezing();
            }
            restBar.value = GameManager.gm.GetFreezeTimePoints();
        }
        if (GameManager.gm.GetFreezeTimePoints() > 0 && !restBar.IsActive())
        {
            restBar.gameObject.SetActive(true);
        }
    }
    void TurnOffFreezing()
    {
        Time.timeScale = 1f;
        this.GetComponent<CharacterController2D>().moveSpeed *= 1f * coeff;
        timeFreezeEnable = false;
    }
}
