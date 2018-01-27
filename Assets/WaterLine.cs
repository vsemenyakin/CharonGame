// Author: Damien Mayance (http://dmayance.com)
// 2013 - Pixelnest Studio (http://pixelnest.io)
// 
// This script simulates a simple 2d water line behavior, like you see in many 2d games.
// See http://dmayance.com/water-line-2d-unity/ for further explanations.
//
// Usage: 
// - Attach it to an object
// - Then fill the "Material" parameter
// - Start the game (here it is only visible at runtime)
//
// The parameters can be modified during runtime, but for EDITOR only.
// The width and height should stay fixed during the game otherwise.
using UnityEngine;
using System.Collections;

public class WaterLinePart
{
  public float _heightOld;
  public float _heightNew;
  public float _speed;

  public float _flow;
  public BuoyancyEffector2D _effector;

  public float height;
  public float velocity;
  public GameObject gameObject;
  public Mesh mesh;
  public Vector2 boundsMin;
  public Vector2 boundsMax;
}

public class WaterLine : MonoBehaviour
{
  public float velocityDamping = 0.999999f; // Proportional velocity damping, must be less than or equal to 1.

  //Geometry
  public float partSize = 1f;
  public float width = 100f;
  public float height = 10f;

  //Physics  
  public float timeScale = 25f;

  //Graphics
  public Material material;
  public Color color = Color.blue;

  //Inner state
  private WaterLinePart[] parts;

  void Start() { Initialize(); }

  private void Initialize() {
    //Setup global state
    material.color = color;
    int thePartsCount = (int)(width/partSize);
    
    parts = new WaterLinePart[thePartsCount];

    //Generate parts
    for (int i = 0; i < thePartsCount; i++) {
      
      //Object & geometry
	  GameObject theGameObject = new GameObject("WavePart_" + i);
      theGameObject.tag = "waterPart";
      theGameObject.transform.parent = this.transform;
      theGameObject.transform.localPosition = new Vector3(i*partSize-width/2, 0, 0);

      //Physics
	  PolygonCollider2D polygonCollider2D = theGameObject.AddComponent<PolygonCollider2D>();
      polygonCollider2D.isTrigger = true;
      polygonCollider2D.usedByEffector = true;

      BuoyancyEffector2D theEffector = theGameObject.AddComponent<BuoyancyEffector2D>();

	  //Graphics
      Mesh mesh = new Mesh();
	  mesh.MarkDynamic();

	  theGameObject.AddComponent<MeshFilter>().mesh = mesh;
	  theGameObject.AddComponent<MeshRenderer>().material = material;

      //Setup part
      parts[i] = new WaterLinePart();
      
	  parts[i]._heightOld = 0.0f;
	  parts[i]._heightNew = 0.0f;
      parts[i]._speed = 0.0f;
	  parts[i]._effector = theEffector;

      parts[i].mesh = mesh;
      parts[i].gameObject = theGameObject;
    }

	for (int i = 0; i < thePartsCount; i++) {
      UpdateMeshVertices(i);
      InitializeTrianglesAndNormalsForMesh(i);      
	}
  }

  private void UpdateMeshVertices(int i) {
    Mesh mesh = parts[i].mesh;
    if (mesh == null) return;

    Transform current = parts[i].gameObject.transform;

    Transform next = current;
    if (i < parts.Length-1) next = parts[i+1].gameObject.transform;

    Vector3 left = Vector3.zero;
    Vector3 right = next.localPosition - current.localPosition;

    // Get all parts of the mesh (it's just 2 planes, one on top and one on the front face)
    Vector3 topLeftFront = new Vector3(left.x, left.y, 0);
    Vector3 topRightFront = new Vector3(right.x, right.y, 0);
    Vector3 topLeftBack = new Vector3(left.x, left.y, 1);
    Vector3 topRightBack = new Vector3(right.x, right.y, 1);
    Vector3 bottomLeftFront = new Vector3(left.x, left.y + (0 - height), 0);
    Vector3 bottomRightFront = new Vector3(right.x, right.y + (0 - height), 0);

    PolygonCollider2D polygonCollider = parts [i].gameObject.GetComponent<PolygonCollider2D> ();
    Vector2[] pointsCollider = new Vector2[]{new Vector2 (left.x, left.y),new Vector2 (right.x, right.y), new Vector2 (right.x, right.y + (0 - height)), new Vector2 (left.x, left.y + (0 - height))};
    polygonCollider.SetPath(0, pointsCollider);

    mesh.vertices = new Vector3[] { topLeftFront, topRightFront, topLeftBack, topRightBack, bottomLeftFront, bottomRightFront };

    parts[i].boundsMin = topLeftFront+current.position;
    parts[i].boundsMax = bottomRightFront+current.position;

    parts[i]._effector.flowMagnitude = parts[i]._flow;
  }

  private void InitializeTrianglesAndNormalsForMesh(int i) {
    Mesh mesh = parts[i].mesh;
    if (mesh == null) return;

    // Normals
    var uvs = new Vector2[mesh.vertices.Length];

    for (int i2 = 0; i2<uvs.Length; i2++) uvs[i2] = new Vector2(mesh.vertices[i2].x, mesh.vertices[i2].z);
    mesh.uv = uvs;

    mesh.triangles = new int[] { 5, 4, 0, 0, 1, 5, 0, 2, 3, 3, 1, 0 };
    mesh.RecalculateNormals(); //For shader
  }

  void Update() {

    //Debug
    if(Input.GetButtonDown ("Fire1")) Splash(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 1.0f);

	//Update model state
	for (int i = 1, size = parts.Length; i < size-1; i++) {
      WaterLinePart theBeforePart = parts[i-1];
	  WaterLinePart thePart = parts[i];
	  WaterLinePart theAfterPart = parts[i+1];

      //Get force	
      float theBeforeDelta = theBeforePart._heightOld-thePart._heightOld;
      float theAfterDelta = theAfterPart._heightOld-thePart._heightOld;
      float theForce = theBeforeDelta+theAfterDelta;
            
      //Update speed
      thePart._speed *= 0.99f;
	  thePart._speed += theForce*0.005f;

      //Update next position
      thePart._heightNew = thePart._heightOld+thePart._speed;
    }

    //Update view
    for (int i = 0, size = parts.Length; i<size; i++) {
      // Update the dot position
      Vector3 newPosition = new Vector3(
          parts[i].gameObject.transform.localPosition.x,
          parts[i]._heightNew,
          parts[i].gameObject.transform.localPosition.z);
      parts[i].gameObject.transform.localPosition = newPosition;
    }

	//Prepare next model state
    for (int i = 0, size = parts.Length; i < size; i++) parts[i]._heightOld = parts[i]._heightNew;

    // Update meshes
    for (int i = 0, size = parts.Length; i < size; i++) UpdateMeshVertices(i);
  }

  #region Interaction

  /// <summary>
  /// Make waves from a point
  /// </summary>
  /// <param name="location"></param>
  /// <param name="force"></param>
//  public void Splash(Vector3 location, int force) {
//    for (int i = 0, size = parts.Length; i<size-1; i++) {
//      if (location.x>=parts[i].boundsMin.x && location.x<parts[i].boundsMax.x &&
//          location.y<=parts[i].boundsMin.y && location.y>parts[i].boundsMax.y)
//                Splash(i, force);
//    }
//  }

  public void Splash(float inX, float inHeight) {
    int theIndex = getPartIndexByPosition(inX);
    if (theIndex < 0 || theIndex > parts.Length) return;

    parts[theIndex-1]._heightOld = -inHeight/2;
    parts[theIndex]._heightOld = -inHeight;
    parts[theIndex+1]._heightOld = -inHeight/2;

    //parts[i].flow = 30;
  }

  private int getPartIndexByPosition(float inPosition) {
    return Mathf.FloorToInt((inPosition + width/2)/partSize);
  }

  #endregion
}