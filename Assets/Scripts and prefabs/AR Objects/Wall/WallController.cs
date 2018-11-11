using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject wallMarker1;
    public GameObject wallMarker2;

    public GameObject wallImageTarget1;
    public GameObject wallImageTarget2;
    private CustomTrackableEventHandler wallScript1;
    private CustomTrackableEventHandler wallScript2;

    public GameObject wallPrefab;
    private GameObject wall;
    private MeshRenderer wallMesh;
    private float WALL_WIDTH = 0.2f;
    private float WALL_HEIGHT = 2.5f;
    private float WALL_LENGTH = 3f;

    public GameObject ship;
    private bool stop = false;

    // Use this for initialization
    void Start () {
        wallScript1 = wallImageTarget1.GetComponent<CustomTrackableEventHandler>();
        wallScript2 = wallImageTarget2.GetComponent<CustomTrackableEventHandler>();
        wall = Instantiate(wallPrefab) as GameObject;
        wall.transform.parent = this.transform;
        wallMesh = wall.GetComponent<MeshRenderer>();        
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (stop) return;
        if (!wallScript1.IsCurrentlyTracked || !wallScript2.IsCurrentlyTracked)
        {
            wallMesh.enabled = false;
            return;
        }

        UpdateShieldWall();
    }

    public void Stop()
    {
        this.stop = true;
        wallMarker1.GetComponent<ProjectionController>().stop = true;
        wallMarker2.GetComponent<ProjectionController>().stop = true;
        UpdateShieldWall();
    }

    private void UpdateShieldWall()
    {
        wallMesh.enabled = true;
        Vector3 delta = wallMarker1.transform.position - wallMarker2.transform.position;
        wall.transform.position = new Vector3(
            wallMarker1.transform.position.x - (delta.x / 2),
            wallMarker1.transform.position.y - (delta.y / 2) + 0.2f,
            wallMarker1.transform.position.z - (delta.z / 2)
         );

        wall.transform.rotation = Quaternion.LookRotation(delta);
        wall.transform.localScale = new Vector3(WALL_WIDTH, WALL_HEIGHT, delta.magnitude * WALL_LENGTH);
    }
}
