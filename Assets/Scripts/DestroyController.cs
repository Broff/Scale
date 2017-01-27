using UnityEngine;
using System.Collections;

public class DestroyController : MonoBehaviour {

    public float destroyTime;
    public bool destroyAfterStart = true;

    void Start()
    {
        if (destroyAfterStart)
        {
            DestroyInit();
        }
    }
	public void DestroyInit(){
	    Destroy(gameObject, destroyTime);
	}
}
