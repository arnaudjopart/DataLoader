using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class MonoBehaviorExtended : MonoBehaviour {

    public string m_type;
    public int m_id;
    public DataLoader m_db;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public virtual void UpdateInfoFromDB(Dictionary<string, Dictionary<int, JsonData>> _dict )
    {
        print("Updated");
        print("Retrieve Info From DataBase branch "+m_type+" - index n°"+m_id+" ...");
        print(_dict[m_type][m_id]["Name"]);
    }
}
