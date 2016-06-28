using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;



public class DataLoader : MonoBehaviour
{
    #region Public and Protected Members
    public string m_ssid;
    public string m_password;
    public string m_spreadSheetName = "_summary";
    public string m_action = "GetData";

    public Dictionary<string, Dictionary<int, JsonData>> m_dataBase = new Dictionary<string, Dictionary<int, JsonData>>();

    public delegate void DBDownloadDataEventHandler(Dictionary<string, Dictionary<int, JsonData>> _dictionary);
    public event DBDownloadDataEventHandler DBOnDownloadDataEvent;

    #endregion
    // Use this for initialization
    void Start()
    {
        DownloadData();        
    }

    
    private string CreateURL(string _sheetName)
    {
        string baseURL = "https://script.google.com/macros/s/AKfycbw9HGdA7fgjTaiBzqmYBI2195PAJCcfQhdoPR5dtcQ7epdU_IM/exec?";
        baseURL += "ssid="+m_ssid;
        baseURL += "&pass=" + m_password;
        baseURL += "&sheet=" + _sheetName;
        baseURL += "&action=" + m_action;
        return baseURL;
    }

    IEnumerator InitialWWWRequest(string _url)
    {

        WWW www = new WWW(_url);

        yield return www;

        if( !string.IsNullOrEmpty( www.error ) )
        { 
            Debug.Log( www.error );

        }
        
        if(www.text == "" )
        {
            print( "no text" );
        }
        
        string[] splitStrings = new string[] {"[","{\"\":\"","\"},{\"\":\"","\"}]" };
        string[] cleanListOfSheets = www.text.Split(splitStrings,StringSplitOptions.RemoveEmptyEntries);

        StartCoroutine(MultipleWWWRequests( cleanListOfSheets ));     

    }

    IEnumerator MultipleWWWRequests(string[] _arrayOfSheets)
    {

        foreach (string sheetName in _arrayOfSheets )
        {
            string url = CreateURL(sheetName);
            WWW www = new WWW(url);

            Debug.Log( "Retrieving data from Sheet " + sheetName );

            yield return www;
          
            string errorString = www.text.Substring( 0, 2 );
            bool checkIfError = errorString == "<!";
            
            if( checkIfError )
            {
                print( "No data in Sheet" );
            }
            else
            {
    
                Dictionary<int, JsonData> subDico = new Dictionary<int, JsonData >();
                string rawData = www.text;
                JsonData BestiaireData = JsonMapper.ToObject(rawData);

                for( int i = 0; i < BestiaireData.Count; i++ )
                {
                    
                    subDico.Add(i, BestiaireData[ i ] );
                }

                m_dataBase.Add( sheetName, subDico );               

            }
            
        }

        isDownloadDone = true;
        StopAllCoroutines();
        DBOnDownloadDataEvent(m_dataBase);
            
    }

    public void DownloadData()
    {
        m_finalURL = CreateURL(m_spreadSheetName);
        m_dataBase = new Dictionary<string, Dictionary<int, JsonData>>();
        StartCoroutine(InitialWWWRequest(m_finalURL));
    }

    #region Private Members

    private string m_finalURL;
    private WWW www;
    
    private bool isDownloadDone = false;

    #endregion

    [Serializable]
    public class Beast
    {
        public string Name;
        public int HP;
        public string Color;

        public static Beast CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Beast>( jsonString );
        }
        
    }
   
}
