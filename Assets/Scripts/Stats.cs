using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] Text kills;
    [SerializeField] Text deaths;

    void Start()
    {

    }

    
    void Update()
    {
        kills.text = PlayerPrefs.GetInt("kills").ToString();
        deaths.text = PlayerPrefs.GetInt("deaths").ToString();
    }
}
