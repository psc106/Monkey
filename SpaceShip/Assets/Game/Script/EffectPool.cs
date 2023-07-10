using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    Effect[] effects;
    // Start is called before the first frame update
    void Start()
    {
        effects = GetComponentsInChildren<Effect>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
