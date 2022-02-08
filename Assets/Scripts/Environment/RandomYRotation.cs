using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomYRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0,300)));

        Destroy(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
