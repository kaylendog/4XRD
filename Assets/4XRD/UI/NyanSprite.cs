using UnityEngine;
using UnityEngine.UI;

namespace _4XRD.UI
{
    [ExecuteInEditMode]
    public class NyanSprite : MonoBehaviour
    {
        Image _image;
        
        void Start()
        {
            _image = GetComponent<Image>();
        }

        void Update()
        {
            _image.color = Color.HSVToRGB(Time.time / 2f % 1f, 0.8f, 1);
        }
    }
}
