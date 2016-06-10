/*
Copyright (c) 2016 Eduardo Farias and Maicon Feldhaus

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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace Lune.Utils
{
    /// <summary>
    /// Static class to save and load persistent data.
    /// </summary>
    public static class DataPersistence
    {
        private static string _dataPath = Application.persistentDataPath;
        private static string _pathPattern = @"" + _dataPath + "/.+";

        /// <summary>
        /// Save a serializable object in a file in persistent data path.
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="data">Serializable object to save</param>
        public static void Save(string filename, object data)
        {
            // data object must to be serializable
            if (!data.GetType().IsSerializable)
            {
                throw new ArgumentException("The data passed is not serializable and therefore is not valid.", "data");
            }

            // save
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(GetPath(filename));
            bf.Serialize(file, data);
            file.Close();
        }

        /// <summary>
        /// Load a serializable object in a file in the persistent data path.
        /// </summary>
        /// <typeparam name="T">Serializable object type to load</typeparam>
        /// <param name="filename">File name</param>
        /// <returns>Returns a serializable object</returns>
        public static T Load<T>(string filename)
        {
            // object must to be serializable
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The object type passed is not serializable and therefore is not valid.", "T");
            }

            // set default value
            T loadedObject = default(T);

            // get path
            string path = GetPath(filename);

            // if exists, load
            if (FileExists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                loadedObject = (T)bf.Deserialize(file);
                file.Close();
            }

            return loadedObject;
        }

        /// <summary>
        /// Check if file exists.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Returns if file exists</returns>
        public static bool FileExists(string filename)
        {
            Match match = Regex.Match(filename, _pathPattern);
            if (match.Success)
                return File.Exists(filename);
            else
                return File.Exists(GetPath(filename));
        }

        /// <summary>
        /// Append to filename a persistent data path.
        /// </summary>
        /// <param name="filename">File name</param>
        /// <returns>Returns a full persistent filename</returns>
        public static string GetPath(string filename)
        {
            return string.Format("{0}/{1}", _dataPath, filename);
        }
    }
}