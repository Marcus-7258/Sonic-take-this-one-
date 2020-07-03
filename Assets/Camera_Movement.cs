using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera_Movement : MonoBehaviour
{
    public GameObject player;
    public float ydistance;
    public float xdistance;
    private Vector3 startoffset;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        startoffset = transform.position - player.transform.position;
        offset = startoffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = player.transform.position;

        if (playerpos.x < xdistance && playerpos.x > 0 && playerpos.y > -2)
        {
            offset = new Vector3(-playerpos.x + startoffset.x, startoffset.y, startoffset.z);
			transform.position = player.transform.position + offset;
        }
        else if (playerpos.x >= xdistance && playerpos.y > -2) 
		{
            offset = new Vector3(-playerpos.x + startoffset.x +(playerpos.x-xdistance), startoffset.y, startoffset.z);
			transform.position = player.transform.position + offset;
        } 
        
    }

}