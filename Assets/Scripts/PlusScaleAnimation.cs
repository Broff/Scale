using UnityEngine;
using System.Collections;

public class PlusScaleAnimation : MonoBehaviour {

    public float speed;
    private bool animate = false;
    private Vector3 target;
	
	// Update is called once per frame
	void Update () {
        if (animate == true)
        {
            if (transform.localScale.x == target.x && transform.localScale.z == target.z)
            {
                animate = false;
            }
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, Time.deltaTime * speed);
        }
	}

    public void Animate(Vector3 t)
    {
        target = t;
        animate = true;
    }
}
