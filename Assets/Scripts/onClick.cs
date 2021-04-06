using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour {

    private void Start() {
        // Transform child = gameObject.transform.Find("onClickHitBox");
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.widthMultiplier = 0f;
    }

    private void OnMouseDown() {
        print("Clicked");
        // Transform child = gameObject.transform.Find("onClickHitBox");
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        // TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        if (trailRenderer.widthMultiplier == 1f) {
            // trailRenderer.emitting = false;
            // trailRenderer.time = 0f;
            trailRenderer.widthMultiplier = 0f;
        } else {
            // trailRenderer.emitting = true;
            // trailRenderer.time = 1000f;
            trailRenderer.widthMultiplier = 1f;
        }
    }
}