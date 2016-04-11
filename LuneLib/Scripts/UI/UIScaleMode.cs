/*
Copyright (c) 2016 Maicon Feldhaus

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial 
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES 
OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Lune.UI
{
    /*
    This script scale the image according its parent.
    */

    [ExecuteInEditMode]
    public class UIScaleMode : MonoBehaviour
    {
        public enum Type
        {
            stretch = 0,
            inside = 1,
            outside = 2
        }

        [SerializeField]
        private Image _image;
        [SerializeField]
        private Type _scaleMode;

        // Use this for initialization
        void Start()
        {
            Resize();
        }

        // Resize the image to scale mode selected
        private void Resize()
        {
            // get image size and border (bounds)
            Vector2 size = _image.sprite.bounds.size * _image.sprite.pixelsPerUnit;
            Vector2 border = transform.parent.GetComponent<RectTransform>().rect.size;

            switch (_scaleMode)
            {
                // stretches the image to fill the area completely in terms of both width and height
                case Type.stretch:
                    _image.preserveAspect = false;
                    _image.rectTransform.sizeDelta = Vector2.zero;
                    _image.rectTransform.localPosition = Vector3.zero;
                    _image.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    break;

                // scales the object proportionally to fit inside the area (its edges will never exceed the bounds of the area)
                case Type.inside:
                    _image.preserveAspect = true;

                    if ((border.x / size.x) < (border.y / size.y)) // vertical
                    {
                        float value = border.x * (size.y / size.x) - border.y;
                        FitVertical(value);
                    }
                    else // horizontal
                    {
                        float value = border.y * (size.x / size.y) - border.x;
                        FitHorizontal(value);
                    }
                    break;

                // scales the image proportionally to completely fill the area,
                // allowing portions of it to exceed the bounds when its aspect ratio doesn't match the area's
                case Type.outside:
                    _image.preserveAspect = true;

                    if ((border.x / size.x) > (border.y / size.y)) // vertical
                    {
                        float value = border.x * (size.y / size.x) - border.y;
                        FitVertical(value);
                    }
                    else // horizontal
                    {
                        float value = border.y * (size.x / size.y) - border.x;
                        FitHorizontal(value);
                    }
                    break;
            }
        }

        // Fits the image horizontally
        private void FitHorizontal(float value)
        {
            float pivot = _image.rectTransform.pivot.x;
            _image.rectTransform.offsetMin = new Vector2(value * -pivot, 0f);
            _image.rectTransform.offsetMax = new Vector2(value * (1f - pivot), 0f);
        }

        // Fits the image vertically
        private void FitVertical(float value)
        {
            float pivot = _image.rectTransform.pivot.y;
            _image.rectTransform.offsetMin = new Vector2(0f, value * -pivot);
            _image.rectTransform.offsetMax = new Vector2(0f, value * (1f - pivot));
        }

#if UNITY_EDITOR
    void OnInspectorGUI()
    {
        _scaleMode = (Type)UnityEditor.EditorGUILayout.EnumPopup("---", _scaleMode);
    }

    void OnDrawGizmos()
    {
        if (enabled && !Application.isPlaying)
            Resize();
    }
#endif
    }
}