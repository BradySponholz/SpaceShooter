using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Challange : MonoBehaviour
{
    public string myName = "Brady";
    public int myAge = 38;
    public float speed = 13f;
    public float health = 95f;
    public int score = 4;
    public bool hasKey = false;
    public int ammoCount = 24;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("My name is: " + myName);
        Debug.Log("My age is: " + myAge);
        Debug.Log("Current Speed: " + speed);
        Debug.Log("Current Health: " + health);
        Debug.Log("Player score: " +  score);
        Debug.Log("Player ammo: " + ammoCount);
        Debug.Log("Boss key aquired: " + hasKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
