using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDeletion : MonoBehaviour {
    public IEnumerator DistanceCheck(List<GameObject> gameObjects) {
        while (true) {
            Vector3 starPosition = GameObject.Find("-1").transform.position;

            List<GameObject> gameObjectsToRemove = new List<GameObject>();
            foreach (GameObject obj in gameObjects) {
                if (System.Math.Abs(Vector3.Distance(obj.transform.position, starPosition)) > 1000000) {
                    print(obj.name + " was delete because it was too far away! (" + Vector3.Distance(obj.transform.position, starPosition) + ")");
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
