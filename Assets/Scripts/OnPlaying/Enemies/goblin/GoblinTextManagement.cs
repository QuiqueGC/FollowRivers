using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoblinTextManagement : MonoBehaviour
{
    [SerializeField] Transform text;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y >= -30)
        {
            text.gameObject.SetActive(true);
        }
        
    }
}
