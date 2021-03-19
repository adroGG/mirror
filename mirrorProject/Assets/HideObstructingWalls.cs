using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObstructingWalls : MonoBehaviour {

    public Transform target;

    public Transform previousObstruction;
    public Transform obstruction;
    public GameObject[] walls;
    public MeshRenderer[] meshes;
    float zoomSpeed = 2f;

    private bool previousWallHiding;

    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("RealCharacter").transform;
        walls = GameObject.FindGameObjectsWithTag("Walls");
        previousWallHiding = false;
    }
    
    private void LateUpdate() {
        // ViewObstructed();
    }

    void ViewObstructed() {
        RaycastHit hit;

        if(previousWallHiding) {
            foreach(GameObject wall in walls) {
                meshes = wall.GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer mesh in meshes)
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            previousWallHiding = false;
        }

        if(Physics.Raycast(transform.position, target.position - transform.position, out hit, 5f )) {
            if(hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Ground") {
                obstruction = hit.transform;
                // obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = 
                //     UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                meshes = obstruction.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes)
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                previousWallHiding = true;

                // ZOOM LOGIC
                if(Vector3.Distance(obstruction.position, transform.position) >= 4f //valor original 3 
                    && Vector3.Distance(transform.position, target.position) >= 0.2f) { //valor original 1.5
                        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            } else {
                // obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = 
                //     UnityEngine.Rendering.ShadowCastingMode.On;

                meshes = obstruction.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes)
                    mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

                // ZOOM LOGIC
                if(Vector3.Distance(transform.position, target.position) < 4f) {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
        }
    }

}
