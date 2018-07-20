using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TerrainMK2{
    private List<float> heightMap;

    public int X { get; set; }
    public int Y { get; set; }

    public List<float> HeightMap
    {
        get { return heightMap; }
    }

    public TerrainMK2(int X, int Y)
    {
        this.X = X;
        this.Y = Y;

        heightMap = ResetHeights(X, Y);
    }

    private List<float> ResetHeights(int w, int h)
    {
        List<float> hm = new List<float>();
        hm.AddRange(Enumerable.Repeat(0.0f, (X+1)*(Y+1)));
        return hm;
    }
}
