using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_script : MonoBehaviour {
    bool isEn = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(printCond());
	}
	
	// Update is called once per frame
	void Update () {
        if (!isEn)
        {
            StartCoroutine(printCond());
        }
	}

    IEnumerator printCond()
    {
        isEn = true;
        yield return new WaitForSeconds(1f);
        isEn = false;
    }
}
