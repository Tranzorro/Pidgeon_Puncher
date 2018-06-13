using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoDeactivator : MonoBehaviour {

    public Image image;

    private void Update()
    {
        //image.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
        image.CrossFadeAlpha(0f, 1f, false);
    }

    void OnBecomeInvisible()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
