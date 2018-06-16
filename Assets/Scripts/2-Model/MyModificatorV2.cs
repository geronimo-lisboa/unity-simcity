using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: Renomear esse arquivo, a cirurgia já foi concluída.
namespace model.terrain
{
    public class Modificator
    {
        private MyTerrainV2 terrain;
        private MyModificationStrategyV2 currentStrategy;
        public float Intensity { get; private set; }
        public Modificator(MyTerrainV2 terrain)
        {
            this.terrain = terrain;
            currentStrategy = null;
            Intensity = 0;
        }

        public void Modify(Ray ray, RaycastHit hit)
        {
            if(currentStrategy==null)
            {
                Debug.LogWarning("Current strategy está null, não fará modificação alguma!");
                return;
            }
            else
            {
                currentStrategy.Execute(terrain, Intensity, hit.point);
            }
        }

        public void SetModificationStrategy(MyModificationStrategyV2 strat)
        {
            this.currentStrategy = strat;
        }

        public void IncreaseIntensity(float v)
        {
            Intensity += v;
        }

        public void ResetIntensity()
        {
            Intensity = 0;
        }
    }
}
