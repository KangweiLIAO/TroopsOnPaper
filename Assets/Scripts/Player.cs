using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public vars:
    public string playerName = "Default", roleName = "Default";

    // private vars:
    private static Hand hand;
    private static Deck deck;
    private static Dictionary<string, int> resources;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        hand = new Hand();
        deck = new Deck();
        resources = new Dictionary<string,int>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
