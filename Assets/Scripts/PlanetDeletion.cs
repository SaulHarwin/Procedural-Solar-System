using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDeletion : MonoBehaviour {
    public IEnumerator DistanceCheck(List<GameObject> gameObjects) {
        while (true) {
            List<GameObject> gameObjectsToRemove = new List<GameObject>();
            foreach (GameObject obj in gameObjects) {
                if (obj.transform.position.x > 10000 || obj.transform.position.y > 10000 || obj.transform.position.z > 10000) {
                    Destroy(obj);
                    gameObjectsToRemove.Add(obj);
                }
            }
            foreach (GameObject obj in gameObjectsToRemove) {
                gameObjects.Remove(obj);
            }
            yield return new WaitForSeconds(1f);
        }
    } 
}
