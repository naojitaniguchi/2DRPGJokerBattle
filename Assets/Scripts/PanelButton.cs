using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour {
    public GameObject toolPanelObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PanelButtonPushed()
    {
        toolPanelObject.SetActive(true);
    }
}
