using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace orisox.com
{
    [System.Serializable]
    public class LightmapRendererData
    {
        public Renderer renderer;
        public int lightmapIndex;
        public Vector4 lightmapScaleOffset;

        public LightmapRendererData(Renderer RendererObj)
        {
            renderer = RendererObj;
            lightmapIndex = RendererObj.lightmapIndex;
            lightmapScaleOffset = RendererObj.lightmapScaleOffset;
        }
    }

    [System.Serializable]
    public class LightmapTextureData
    {
        public Texture2D lightmapNear;
        public Texture2D lightmapFar;

        public LightmapTextureData(LightmapData Data)
        {
            lightmapNear = Data.lightmapNear;
            lightmapFar = Data.lightmapFar;
        }
    }

    [ExecuteInEditMode]
    public class LightmapSave : MonoBehaviour
    {
        public List<LightmapTextureData> textureData;
        public List<LightmapRendererData> rendererData;

        public void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.Lightmapping.completed += Save;
#endif
        }

        public void OnDisable()
        {
#if UNITY_EDITOR
            UnityEditor.Lightmapping.completed -= Save;
#endif
        }

        public void Save()
        {
            Clear();

            if (null != LightmapSettings.lightmaps)
            {
                textureData = new List<LightmapTextureData>();
                for (int i = 0; i < LightmapSettings.lightmaps.Length; ++i)
                {
                    textureData.Add(new LightmapTextureData(LightmapSettings.lightmaps[i]));
                }

                rendererData = new List<LightmapRendererData>();
                UpdatelightmapRendererData(transform, rendererData);
            }
        }

        void UpdatelightmapRendererData(Transform Trans, List<LightmapRendererData> RendererData)
        {
            var RendererObj = Trans.GetComponent<Renderer>();
            if (null != RendererObj && -1 != RendererObj.lightmapIndex)
            {
                RendererData.Add(new LightmapRendererData(RendererObj));
            }

            for (int i = 0; i < Trans.childCount; ++i)
            {
                var Child = Trans.GetChild(i);
                UpdatelightmapRendererData(Child, RendererData);
            }
        }

        public void ShowLightmap(int StartIndex)
        {
            if (null != rendererData)
            {
                foreach (var Lightmap in rendererData)
                {
                    if (null != Lightmap.renderer)
                    {
                        Lightmap.renderer.lightmapIndex = StartIndex + Lightmap.lightmapIndex;
                        Lightmap.renderer.lightmapScaleOffset = Lightmap.lightmapScaleOffset;
                    }
                }
            }
        }

        public void Clear()
        {
            textureData = null;
            rendererData = null;
        }
    }
}
