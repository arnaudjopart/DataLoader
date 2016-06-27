using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour
{
    #region Public and Protected Members
    public string m_ssid;
    public string m_password;
    public string m_spreadSheetName;
    public string m_action = "GetData";

    #endregion
    // Use this for initialization
    void Start()
    {
        m_finalURL = CreateURL();
        StartCoroutine( "WWWRequest" );
    }

    // Update is called once per frame
    void Update()
    {
        if( www != null )
        {
            print( www.progress );

        }
        
    }
    private string CreateURL()
    {
        string baseURL = "https://script.google.com/macros/s/AKfycbw9HGdA7fgjTaiBzqmYBI2195PAJCcfQhdoPR5dtcQ7epdU_IM/exec?";
        baseURL += "ssid="+m_ssid;
        baseURL += "&pass=" + m_password;
        baseURL += "&sheet=" + m_spreadSheetName;
        baseURL += "&action=" + m_action;

        return baseURL;
    }

    
    IEnumerator WWWRequest()
    {
        WWW www = new WWW("https://script.google.com/macros/s/AKfycbw9HGdA7fgjTaiBzqmYBI2195PAJCcfQhdoPR5dtcQ7epdU_IM/exec?ssid=1YHDIhSNy-fTEszQGd_5SK-VSzG-AxwtBzfsDY1HIeE0&pass=Chaussette1&sheet=BESTIAIRE&action=GetData");
        yield return www;

        if( !string.IsNullOrEmpty( www.error ) )
        { 
            Debug.Log( www.error );

        }
       
        if(www.text == "" )
        {
            print( "no text" );
        }
        print( www.data );
        
    }
    #region Private Members
    private string m_finalURL;
    private WWW www;
    #endregion
}
