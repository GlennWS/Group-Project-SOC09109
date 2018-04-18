using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createObjs : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<Vector3> prefabAngles;

    public Rect Bounds;

    public Vector3 vel;

    public float timeToNext;

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(1, 500, 299);
        vel = new Vector3(70f, 0f, -10f);

        timeToNext = Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.z > 0)
        {
            if (timeToNext <= 0f)
            {
                timeToNext = Random.Range(0.5f, 1.5f);

                Ray ray = new Ray(transform.position, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    int idx = Random.Range(0, prefabs.Count);
                    Instantiate(prefabs[idx], hit.point, Quaternion.Euler(prefabAngles[idx]));
                }
            }
            else
            {
                timeToNext -= Time.deltaTime;
            }

            if (!Bounds.Contains(new Vector2(transform.position.x, transform.position.z)))
            {
                vel.x *= -1f;
            }

            transform.position += vel * Time.deltaTime;
        }
        
	}
}
