using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCreation : MonoBehaviour {    
    GameObject body;
    public AnimationCurve massCurve;
    [Range(0.0001f, 10f)]
    public float timeScale;

    public GameObject MainCamera;
    public int PlanetCount;
    public int initialSpaceSize;
    public float velocityMagnitude;
    public float massMagnitude;

    List<GameObject> gameObjects = new List<GameObject>();

    void Start() {
        Time.timeScale = timeScale;

        for (int i = 0; i < PlanetCount; i ++) {
			float mass = Random.Range(0f, 1f);
            mass = massCurve.Evaluate(mass);
            mass *= massMagnitude;
            Vector3 position = new Vector3(Random.Range(-initialSpaceSize, initialSpaceSize), Random.Range(-initialSpaceSize, initialSpaceSize), Random.Range(-initialSpaceSize, initialSpaceSize));
			Vector3 force = new Vector3(Random.Range(-velocityMagnitude, velocityMagnitude), Random.Range(-velocityMagnitude, velocityMagnitude), Random.Range(-velocityMagnitude, velocityMagnitude));	

            GameObject prefab = (GameObject)Resources.Load("Body");
            body = Instantiate(prefab, position, Quaternion.identity);
            body.transform.name = "" + i;    
			body.transform.localScale = new Vector3(mass/100, mass/100, mass/100);
            
            Rigidbody rb = body.GetComponent<Rigidbody>();
			rb.mass = mass; 
			rb.AddForce(force);

            Transform child = body.transform.Find("onClickHitBox");
            TrailRenderer trailRenderer = body.gameObject.GetComponent<TrailRenderer>();
            trailRenderer.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            gameObjects.Add(body);
        }

        StartCoroutine(MainCamera.GetComponent<PlanetDeletion>().DistanceCheck(gameObjects));
    }

    private void Update() {
        if (body == null) {
            return;
        }
        Time.timeScale = timeScale;    
        Rigidbody rb = body.GetComponent<Rigidbody>();
        LineRenderer lineRenderer = body.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, rb.velocity);
    }
}
