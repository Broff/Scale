using UnityEngine;
using System.Collections;

public class EnviromentController : MonoBehaviour {

    public GameObject skinsController;
    public GameObject gameController;

    public float scaleStartPlatform;
    public float posStartPlatform;
    public float posPlatform;

    private Skin s;
    GameObject platform = null;
    GameObject startPplatform = null;

    public void initAll()
    {
        s = skinsController.GetComponent<SkinsController>().GetActive();
        initPlatform();
        initStartPlatform();
    }

    private void initPlatform()
    {
        if (platform != null)
        {
            Destroy(platform);
        }
        gameController.GetComponent<GameController>().platformPrefab = s.prefab;
        platform = Instantiate(s.prefab);
        platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, posPlatform, platform.transform.localPosition.z);
        platform.SetActive(false);
        gameController.GetComponent<GameController>().activePlatform = platform;
        gameController.GetComponent<GameController>().coloredObj[0] = platform;
    }

    private void initStartPlatform()
    {
        if (startPplatform != null)
        {
            Destroy(startPplatform);
        }
        startPplatform = Instantiate(s.prefabStartPlatform);
        startPplatform.transform.localScale = new Vector3(startPplatform.transform.localScale.x, scaleStartPlatform, startPplatform.transform.localScale.z);
        startPplatform.transform.localPosition = new Vector3(startPplatform.transform.localPosition.x, posStartPlatform, startPplatform.transform.localPosition.z);
        startPplatform.GetComponent<ScaleController>().enabled = false;
        gameController.GetComponent<GameController>().coloredObj[1] = startPplatform;
    }
}
