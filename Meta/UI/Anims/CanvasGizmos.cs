using TMPro;
using UnityEngine;

namespace Meta.UI.Anims
{
    [ExecuteInEditMode]
    public class CanvasGizmos : MonoBehaviour
    {
        [Header("Text")]
        public string gizmoName;
        public float fontSize = 350;
        public float topPadding = 400;
        public TextAlignmentOptions txtAlign;
        public TMP_FontAsset font;
        public TextMeshProUGUI txt;


        [Header("Color")]
        public Color clr;
        [SerializeField] Canvas canvasParent;
        [Header("Inspector")]
        public bool autoOffset;
        public int xOffset;
        public int yOffset;
        [Header("Resolution info:")]
        public float width;
        public float height;
        RectTransform _rt;
        float _x, _y;
        void Start()
        {
            _rt = GetComponent<RectTransform>();
            if (!Application.isPlaying) return;
            if (!txt)
                txt = GetComponent<TextMeshProUGUI>();
            Destroy(txt);
        }
     
#if UNITY_EDITOR


        void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (!_rt)
                _rt = GetComponent<RectTransform>();

            if (!_rt) return;

            width = _rt.rect.width * canvasParent.transform.localScale.x;
            height = _rt.rect.height * canvasParent.transform.localScale.y;

            if (autoOffset)
            {
                var xNew = xOffset * width;
                var yNew = yOffset * height;
                var movePos = new Vector3(xNew, yNew, 0);

                if (_rt.localPosition != movePos)
                {
                    _rt.localPosition = movePos;
                }
            }

            _x = transform.position.x;
            _y = transform.position.y;
            DrawQuadrant();


            if (!txt)
            {
                txt = GetComponent<TextMeshProUGUI>();
                if (!txt)
                {
                    txt = gameObject.AddComponent<TextMeshProUGUI>();
                }
            }

            if (txt)
            {
                txt.fontSize = fontSize;
                txt.text = gizmoName;
                txt.alignment = txtAlign;
                txt.font = font;
                txt.enableWordWrapping = false;
                txt.margin = new Vector4(0, -topPadding, 0, 0);
            }
        }

        void DrawQuadrant()
        {
            var w = width / 2;
            var h = height / 2;

            var a = new Vector2(_x - w, _y - h);
            var b = new Vector2(_x - w, _y + h);
            var c = new Vector2(_x + w, _y + h);
            var d = new Vector2(_x + w, _y - h);

            Debug.DrawLine(a, b, clr);
            Debug.DrawLine(b, c, clr);
            Debug.DrawLine(c, d, clr);
            Debug.DrawLine(d, a, clr);
        }


#endif
    }
}