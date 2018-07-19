using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TerrainMK2{
    private List<float> heightMap;
    public int Size { get; private set; }

    public List<float> HeightMap
    {
        get { return heightMap; }
    }

    public TerrainMK2(int size)
    {
        Size = size;
        heightMap = ResetHeights(size, size);
    }

    private List<float> ResetHeights(int w, int h)
    {
        List<float> hm = new List<float>();
        hm.AddRange(Enumerable.Repeat(0.0f, Size*Size));
        return hm;
    }
}
