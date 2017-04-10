using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace orisox.com
{
    public class LightmapLoad
    {
        public static void LoadLightmap(List<LightmapSave> Loaders)
        {
            ClearLightmap();

            int LightmapDataCount = 0;
            for (int i = 0; i < Loaders.Count; ++i)
            {
                LightmapDataCount += (null != Loaders[i].textureData) ? Loaders[i].textureData.Count : 0;
            }

            if (0 < LightmapDataCount)
            {
                var LightmapDatas = new LightmapData[LightmapDataCount];
                int Index = 0;
                for (int i = 0; i < Loaders.Count; ++i)
                {
                    var Loader = Loaders[i];
                    Loader.ShowLightmap(Index);
                    var TextureData = Loader.textureData;
                    if (null != TextureData)
                    {
                        for (int j = 0; j < TextureData.Count; ++j, ++Index)
                        {
                            LightmapDatas[Index] = new LightmapData();
                            LightmapDatas[Index].lightmapNear = TextureData[j].lightmapNear;
                            LightmapDatas[Index].lightmapFar = TextureData[j].lightmapFar;
                        }
                    }
                }

                LightmapSettings.lightmaps = LightmapDatas;
            }
            else
            {
                LightmapSettings.lightmaps = null;
            }
        }

        [ContextMenu("Clear Lightmap")]
        public static void ClearLightmap()
        {
            LightmapSettings.lightmaps = null;
        }
    }
}
