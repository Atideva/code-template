using System.Linq;
using TMPro;
using UnityEngine;

namespace Meta.UI.Anims
{
    public class ColorNumbers : MonoBehaviour
    {
        [SerializeField] TMP_Text txt;
        [SerializeField] bool applyFromStart;
        [SerializeField] Color color;

        void Start()
        {
            if (applyFromStart)
                ApplyColor(color);
        }

        public void ApplyColor() => ApplyColor(color);

        public void ApplyColor(Color clr)
        {
            txt.ForceMeshUpdate();

            var textInfo = txt.textInfo;
            var numbers =
                (from info in textInfo.characterInfo where IsNumber(info.character) select info.index).ToArray();

            foreach (var i in numbers)
            {
                var materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                var newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                var vertexIndex = textInfo.characterInfo[i].vertexIndex;

                if (!textInfo.characterInfo[i].isVisible) continue;

                Color32 c0 = clr;
                newVertexColors[vertexIndex + 0] = c0;
                newVertexColors[vertexIndex + 1] = c0;
                newVertexColors[vertexIndex + 2] = c0;
                newVertexColors[vertexIndex + 3] = c0;

                txt.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }

        static bool IsNumber(char c) =>
            c.ToString() switch
            {
                "0" => true,
                "1" => true,
                "2" => true,
                "3" => true,
                "4" => true,
                "5" => true,
                "6" => true,
                "7" => true,
                "8" => true,
                "9" => true,
                _ => false
            };
    }
}