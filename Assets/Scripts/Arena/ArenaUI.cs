using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUI : MonoBehaviour {

    public Text EnemyNameText;

    private static ArenaUI instance;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SetEnemyName(string name)
    {
        instance.EnemyNameText.text = name;
    }
}
