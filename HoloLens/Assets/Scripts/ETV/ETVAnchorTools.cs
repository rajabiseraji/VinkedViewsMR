﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tools
{
    ROTATE, SCALE, TRANSLATE
}

public class ETVAnchorTools : MonoBehaviour {

    public GameObject GadgetTranslate;
    public GameObject GadgetRotate;
    public GameObject GadgetScale;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableTool(Tools tool)
    {
        DisableAllTools();

        switch (tool)
        {
            case Tools.ROTATE: GadgetRotate.SetActive(true); break;
            case Tools.SCALE: GadgetScale.SetActive(true); break;
            default: GadgetTranslate.SetActive(true); break;
                
        }
    }

    public void DisableAllTools()
    {
        GadgetTranslate.SetActive(false);
        GadgetScale.SetActive(false);
        GadgetRotate.SetActive(false);
    }
}
