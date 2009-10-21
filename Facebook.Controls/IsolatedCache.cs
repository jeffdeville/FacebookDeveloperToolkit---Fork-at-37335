using System;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Diagnostics;


namespace Facebook.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class IsolatedCache
    {
        const string keyHashFileName = "hash.xml";

        static public string ApiKey = "ApiKey";
        static public string ApplicationSecret = "ApplicationSecret";
        static public string SessionKey = "SessionKey";
        static public string SessionSecret = "SessionSecret";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData(string key, string value)
        {
            lock (this)
            {
                // need to refresh before trying access
                Dictionary<string, string> keyHash = LoadKeyValueData();

                // Always remove if value=null
                if (keyHash.ContainsKey(key))
                    keyHash.Remove(key);

                if (!string.IsNullOrEmpty(value))
                {
                    keyHash.Add(key, value);
                }

                SaveKeyValueData(keyHash);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetData(string key)
        {
            lock (this)
            {
                // need to refresh before trying access
                Dictionary<string, string> keyHash = LoadKeyValueData();

                if (keyHash.ContainsKey(key))
                {
                    return keyHash[key];
                }
                return null;
            }
        }


        static private void SaveData(string data, string fileName)
        {
            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(fileName, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(isfs))
                        {
                            sw.Write(data);
                            sw.Close();
                        }
                    }
                }
            }
            catch (IsolatedStorageException)
            {

            }

        }

        static private string LoadData(string fileName)
        {
            string data = null;

            try
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists(fileName))
                    {
                        using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                        {
                            using (StreamReader sr = new StreamReader(isfs))
                            {
                                string lineOfData = String.Empty;
                                while ((lineOfData = sr.ReadLine()) != null)
                                    data += lineOfData;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (IsolatedStorageException)
            {
            }

            return data;
        }

        static Dictionary<string, string> LoadKeyValueData()
        {
            Dictionary<string, string> keyHash = new Dictionary<string, string>();

            string data = LoadData(keyHashFileName);

            if (data != null)
            {
                XDocument xmlResponse = XDocument.Parse(data);

                foreach (XElement e in xmlResponse.Root.Descendants())
                {
                    keyHash.Add(e.Name.LocalName, e.Value);
                }
            }

            return keyHash;
        }

        void SaveKeyValueData(Dictionary<string, string> keyHash)
        {
            XDocument doc = new XDocument();
            XElement root = new XElement("Root");
            doc.Add(root);

            foreach (var kvp in keyHash)
            {
                root.Add(new XElement(kvp.Key, kvp.Value));
            }

            SaveData(doc.ToString(), keyHashFileName);
        }

    }
}
