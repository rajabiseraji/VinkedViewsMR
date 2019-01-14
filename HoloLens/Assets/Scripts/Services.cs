﻿using ETV;
using MetaVisualization;
using UnityEngine;
using VisBridges;

/// <summary>
/// Service locator. Loose implementation of the design pattern 
/// Service Locator. Gives access to all important system services
/// and singletons. Populate in editor!
/// </summary>
public class Services : MonoBehaviour
{
    public static Services instance = null;

    [SerializeField]
    public Graphical3DPrimitiveFactory Factory3DPrimitives;
    public Graphical2DPrimitiveFactory Factory2DPrimitives;
    public ETV3DFactory Factory3DETV;                      
    public ETV2DFactory Factory2DETV;
    public VisualizationFactory visualizationFactory;
    public AMetaVisSystem metaVisSystem;
    public AMetaVisFactory FactoryMetaVis;
    public AVisBridgeSystem VisBridgeSystem;
    public APersistenceManager PersistenceManager;
    public DataProvider dataProvider;

    public ClientManager clientManager;

    public static AVisBridgeSystem VisBridgeSys()
    {
        if(instance.VisBridgeSystem == null)
        {
            instance.VisBridgeSystem = new NullVisBridgeSystem();
        }
        return instance.VisBridgeSystem;
    }

    public static Graphical3DPrimitiveFactory PrimFactory3D()
    {
        return instance.Factory3DPrimitives;
    }

    public static DataProvider DataBase()
    {
        if(instance.dataProvider == null)
        {
            throw new System.Exception("Data Provider must be provided to the ServiceLocator.");
        }
        instance.dataProvider.Initialize();
        return instance.dataProvider;
    }

    public static Graphical2DPrimitiveFactory PrimFactory2D()
    {
        return instance.Factory2DPrimitives;
    }

    public static ETV3DFactory ETVFactory3D()
    {
        return instance.Factory3DETV;
    }

    public static ETV2DFactory ETVFactory2D()
    {
        return instance.Factory2DETV;
    }

    public static AMetaVisSystem MetaVisSys()
    {
        return instance.metaVisSystem;
    }

    public static AMetaVisFactory MetaVisFactory()
    {
        return instance.FactoryMetaVis;
    }

    public static VisualizationFactory VisFactory()
    {
        instance.visualizationFactory.Initialize();
        return instance.visualizationFactory;
    }

    public static APersistenceManager Persistence()
    {
        instance.PersistenceManager.Initialize();
        return instance.PersistenceManager;
    }


    void Awake()
    {
        //Physics.autoSimulation = false;
        // SINGLETON

        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
}
