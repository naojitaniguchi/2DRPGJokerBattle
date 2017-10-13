using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Novel;

public class SceneChangeToJocker : MonoBehaviour {
	public string scenario = "wide/scene1" ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeToJocker(){
		NovelSingleton.StatusManager.callJoker(scenario,"");
	}
}
