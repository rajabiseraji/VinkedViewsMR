﻿using System.Collections;
using System.Collections.Generic;
using ETV.ETV3D;
using Model;
using UnityEngine;

public class ETV3DFactory : AETVFactory
{
    public GameObject ETVAnchor;

    public GameObject etv3DBarChart;
    public GameObject etv3DGroupedBarChart;
    public GameObject barChartLegend3D;
    public GameObject ETV3DScatterPlotPrefab;
    public GameObject ETV3DParallelCoordinatesPlotPrefab;
    public GameObject ETV3DBarMapPrefab;

    public override GameObject CreateETVBarChart(DataSet data, int attributeID)
    {
        GameObject barChart = Instantiate(etv3DBarChart);

        barChart.GetComponent<ETV3DBarChart>().Init(data, attributeID);

        return barChart;
    }

    public GameObject Create3DGroupedBarChart(IDictionary<string, DataObject> data)
    {
        GameObject barChart = Instantiate(etv3DGroupedBarChart);

        barChart.GetComponent<ETV3DGroupedBarChart>().Init(data);

        return barChart;
    }

    public GameObject Create3DBarChartLegend(string[] names, Color[] colors)
    {
        GameObject legend = Instantiate(barChartLegend3D);

        legend.GetComponent<BarChartLegend3D>().Init(names, colors);

        return legend;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override GameObject CreateETVLineChart(DataSetLines data, float minX, float maxX, float minY, float maxY, float ticksX, float ticksY)
    {
        throw new System.NotImplementedException();
    }

    public override GameObject CreateETVScatterPlot(DataSetPoints data, float[] mins, float[] maxs, float[] ticks)
    {
        GameObject scatterPlot3D = Instantiate(ETV3DScatterPlotPrefab);
        scatterPlot3D.GetComponent<ETV3DScatterPlot>().Init(data, mins, maxs, ticks);

        return scatterPlot3D;
    }

    public override GameObject CreateETVParallelCoordinatesPlot(DataSetMultiDimensionalPoints data, float[] ticks)
    {
        GameObject pcp = Instantiate(ETV3DParallelCoordinatesPlotPrefab);

        pcp.GetComponent<ETV3DParallelCoordinatesPlot>().Init(data, ticks);
        pcp.GetComponent<ETV3DParallelCoordinatesPlot>().UpdateETV();

        return pcp;
    }

    public GameObject CreateETVBarMap(DataSetMatrix2x2Nominal data, float ticks)
    {
        GameObject bm = Instantiate(ETV3DBarMapPrefab);

        bm.GetComponent<ETV3DBarMap>().Init(data, ticks);

        return bm;
    }

    public override GameObject PutETVOnAnchor(GameObject ETV)
    {
        GameObject Anchor = Instantiate(ETVAnchor);
        Anchor.GetComponent<ETVAnchor>().PutETVintoAnchor(ETV);
        return Anchor;
    }
}
