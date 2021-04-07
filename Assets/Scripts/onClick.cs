using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour {

    private void Start() {
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.widthMultiplier = 0f;
    }

    private void OnMouseDown() {
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        if (trailRenderer.widthMultiplier == 1f) {
            trailRenderer.widthMultiplier = 0f;
        } else {
            trailRenderer.widthMultiplier = 1f;
        }
    }
}