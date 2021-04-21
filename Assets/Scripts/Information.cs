using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Information : MonoBehaviour {
    [Header("How long it takes to update the information")]
    [Range(0.001f, 10f)]
    public float updateTime;

    [Header("Number of Planet currently in Game")]
    [Tooltip("Amount of planet in scene. Updated Every 5 seconds.")]
    public int planetCount; 
        
    [Header("Star Information")]
    public float speed; 
    public float mass;

    bool allTrails = false;

    private void Start() {
        StartCoroutine(PlanetCounter());
        StartCoroutine(StarInformation());
    }

    public void EnableAllTrails() {
        allTrails = !allTrails;

        GameObject celestialBodies = GameObject.Find("Celestial Bodies");
        List<GameObject> wasVisible = new List<GameObject>();

        if (allTrails) {
            foreach (Transform body in celestialBodies.transform) {   
                if (body.gameObject.GetComponent<TrailRenderer>().widthMultiplier == 3f) {
                    wasVisible.Add(body.gameObject);
                } else {
                    body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 3.001f;
                }
            }
        } else {
            foreach (Transform body in celestialBodies.transform) {   
                if (body.gameObject.GetComponent<TrailRenderer>().widthMultiplier == 3.001f) {
                    body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 0f;
                }
            }
        }
    }

    public void DisableAllTrails() {
        GameObject celestialBodies = GameObject.Find("Celestial Bodies");
        foreach (Transform body in celestialBodies.transform) {
            body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 0f;
        }
    }

    public void DeleteAllTrails() {
        GameObject celestialBodies = GameObject.Find("Celestial Bodies");
        foreach (Transform body in celestialBodies.transform) {
            float width = body.gameObject.GetComponent<TrailRenderer>().widthMultiplier;
            body.gameObject.GetComponent<TrailRenderer>().Clear();
            body.gameObject.GetComponent<TrailRenderer>().widthMultiplier = width;
        }
    }

    public void IncreaseTime() {
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<PlanetCreation>().timeScale += 0.1f;
    }

    public void DecreaseTime() {
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<PlanetCreation>().timeScale -= 0.1f;
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