using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace model.terrain.terrainModificationStrategy
{


    public class GaussianTerrainModificationStrategy : TerrainModificationStrategy
    {
        Texture2D CreateTexture(float [,] mat)
        {
            int xLen = (int)Mathf.Sqrt(mat.Length);
            var texture = new Texture2D(xLen, xLen, TextureFormat.ARGB32, false);
            for(var z=0; z<xLen; z++)
            {
                for(var x=0; x< xLen; x++)
                {
                    Color c = new Color(mat[z, x], mat[z, x], mat[z, x], mat[z, x]);
                    texture.SetPixel(z, x, c);
                }
            }
            texture.Apply();
            return texture;
        }

        void SaveTextureToFile(Texture2D texture, string fileName)
        {
            var bytes = texture.EncodeToPNG();
            var file = File.Open(Application.dataPath + "/" + fileName, FileMode.Create);
            var binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();
        }


        public GaussianTerrainModificationStrategy(ElevationChange raiseOrLower) : base(raiseOrLower)
        {
        }

        private float EvaluateGaussian(int[] center, int[] position, float σx = 1, float σy = 1, float A = 1)
        {
            //Implementação BEEEM mastigada do gaussiano, focada em clareza e não velocidade
            float dX = Mathf.Pow(position[0] - center[0], 2); //o (x-x0)^2 da equação
            float dY = Mathf.Pow(position[1] - center[1], 2); //o (y-y0)^2 da equação

            float XdividedBySigma = dX / (2 * Mathf.Pow(σx, 2));
            float YdividedBySigma = dY / (2 * Mathf.Pow(σy, 2));

            float XPlusY = -1 * (XdividedBySigma + YdividedBySigma);

            float H = A * Mathf.Exp(XPlusY);

            return H;
        }
        
        //        https://en.wikipedia.org/wiki/Gaussian_function
        //        Olhar a sessão de gaussiano 2d
        public override void Execute(MyTerrain terrain, float intensity, Vector3 pointInWC)
        {
            int[] pointInImageCoordinate = WorldCoordinateToImageCoordinate(terrain, pointInWC);
            //pointInImageCoordinate é o centro da função gaussiana
            List<float> gaussianMaskPoints = new List<float>();
            int width = terrain.Heightmap.width;//Assumo quadrado
            float sigmaX = intensity * 10.0f;
            float sigmaY = intensity * 10.0f;
            float A = intensity / 50;
            Texture2D tex = terrain.Heightmap;
            for (int x=0; x<width; x++)
            {
                for(int z=0; z<width; z++)
                {
                    int[] p = new int[] {x,z };
                    float v = EvaluateGaussian(pointInImageCoordinate, p, sigmaX, sigmaY, A);
                    Color oldColor = terrain.Heightmap.GetPixel(x, z);
                    Color valueToAdd = new Color(v, v, v);
                    Color newColor;
                    if(this.RaiseOrLower==ElevationChange.Lower)
                    {
                        newColor = oldColor - valueToAdd;
                    }
                    else
                    {
                        newColor = oldColor + valueToAdd;
                    }
                    tex.SetPixel(x, z, newColor);
                }
            }
            tex.Apply();
            terrain.Heightmap = tex;
            SaveTextureToFile(tex, "/Debug/debug" + (++ct) + ".png");
        }
        int ct = 0;
    }
}
