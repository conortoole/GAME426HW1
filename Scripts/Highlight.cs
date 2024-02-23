using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highlights
{
    public class Highlight: MonoBehaviour
    {
        [SerializeField]
        public Color color = Color.red;
        private Renderer lastTileRenderer;
        Renderer hitRenderer;
        RaycastHit hit;

        public void HighlightObject(Renderer renderer, bool val){
            Material[] materials = renderer.materials;

            if (renderer != null && val == true){
                foreach (var material in materials){
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", color);
                }
            }
            else{
                foreach (var material in materials){
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}