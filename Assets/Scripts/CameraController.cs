using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float startPosY;
    public float moveSpeed;
    public float deltaY;
    public float gameOverZ;
    private Vector3 nextPosition;

	void Start () {
        nextPosition = transform.position;
        SetY(startPosY);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * moveSpeed);
	}

    public void SetY(float y)
    {
        nextPosition = new Vector3(transform.position.x, y + deltaY, transform.position.z);
    }

    public void GameOver()
    {
        nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + gameOverZ);
    }
}
