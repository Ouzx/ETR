using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    public static NameGenerator instance;
    void Awake()
    {
        if (instance == null) instance = this;
    }
    string[] names = {
        "Bucket", "Blue", "Ice", "Hurricane", "Pyro",
        "Mugs", "Cyclone", "Black Widow", "Babe",
        "Lightning", "Bad Boy", "Wiggles", "Pops",
        "Artsy", "Sage", "Silence", "Rouge", "Dino",
        "Handsome", "Daring",};

    public string GetName() => names[Random.Range(0, names.Length)];


}
