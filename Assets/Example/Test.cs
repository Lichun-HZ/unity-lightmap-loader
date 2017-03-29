using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public LightmapSave obj0;
    public LightmapSave obj1;

    public void Start()
    {
        var Objs = new List<LightmapSave>();
        Objs.Add(GameObject.Instantiate<LightmapSave>(obj0));
        Objs.Add(GameObject.Instantiate<LightmapSave>(obj1));
        LightmapLoad.LoadLightmap(Objs);
    }
}
