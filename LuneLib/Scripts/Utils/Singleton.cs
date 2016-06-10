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

namespace Lune.Utils
{
    /// <summary>
    /// Abstract MonoBehaviour singleton class.
    /// Keep an unique and indestructible game object.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            // check if instance already exists
            if (_instance == null)
                // if not, set instance to this
                _instance = GetComponent<T>();

            // if instance already exists and it's not this
            else if (_instance != this)
                // then destroy this, meaning there can only ever be one instance of a T
                Destroy(gameObject);

            // sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }
    }
}