using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class Monster : MonoBehaviorExtended {
    

    // Use this for initialization


    void Start () {
        
        m_db.DBOnDownloadDataEvent += UpdateInfoFromDB;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Private Members

    #endregion
}
