using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Information : MonoBehaviour {
    [Header("How long it takes to update the information")]
    [Range(0.001f, 10f)]
    public float updateTime;

    [Space]
    [SerializeField]
    [TextArea]
    private string Desciption;

    [Header("Number of Planet currently in Game")]
    [Tooltip("Amount of planet in scene. Updated Every 5 seconds.")]
    public int planetCount; 
        
    [Header("Star Information")]
    public float speed; 
    public float mass;

    public bool allTrails;

    private void Start() {
        StartCoroutine(PlanetCounter());
        StartCoroutine(StarInformation());
        StartCoroutine(AllTrailsChanged());
    }

    IEnumerator AllTrailsChanged() {
        bool previousAllTrails = false;
        while(true) {
            if (allTrails != previousAllTrails) {
                print("Changed");
                GameObject celestialBodies = GameObject.Find("Celestial Bodies");
                List<GameObject> wasVisible = new List<GameObject>();

                if (allTrails) {
                    foreach (Transform body in celestialBodies.transform) {   
                        if (body.gameObject.GetComponent<TrailRenderer>().widthMultiplier == 1f) {
                            wasVisible.Add(body.gameObject);
                        } else {
                            body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 1.001f;
                        }
                    }
                } else {
                    foreach (Transform body in celestialBodies.transform) {   
                        if (body.gameObject.GetComponent<TrailRenderer>().widthMultiplier == 1.001f) {
                            body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 0f;
                        }
                    }
                }
                previousAllTrails = allTrails;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator PlanetCounter() {
        while (true) {
            GameObject celestialBodies = GameObject.Find("Celestial Bodies");
            planetCount = celestialBodies.transform.childCount;
            yield return new WaitForSeconds(updateTime);
        } 
    }

    IEnumerator StarInformation() {
        while (true) {
            GameObject star = GameObject.Find("-1");
            mass = star.GetComponent<Rigidbody>().mass;
            speed = star.GetComponent<Rigidbody>().velocity.magnitude;
            yield return new WaitForSeconds(updateTime);
        } 
    }
}
