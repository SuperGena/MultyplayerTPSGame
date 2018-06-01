using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int Speed;
    Vector3 lastPos;
    public GameObject decal;

	void Start ()
    {
        lastPos = transform.position;
	}
	
	
	void Update ()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        RaycastHit hit;

        Debug.DrawLine(lastPos, transform.position);
        if (Physics.Linecast(lastPos, transform.position, out hit))
        {
            print(hit.transform.name);

            GameObject d = Instantiate<GameObject>(decal);
            d.transform.position = hit.point + hit.normal * 0.001f;
            d.transform.rotation = Quaternion.LookRotation(-hit.normal);
            Destroy(d, 10);

            Destroy(gameObject);
        }
        lastPos = transform.position;
	}
}
