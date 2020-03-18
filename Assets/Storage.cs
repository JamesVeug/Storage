using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    public interface ISaveable
    {
        void Save();
    }
    
    public static Storage instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new Storage();
            }

            return m_instance;
        }
    }
    private static Storage m_instance;

    private List<ISaveable> m_queue = new List<ISaveable>();

    private void Update()
    {
        if (m_queue.Count == 0)
        {
            return;
        }

        for (int i = 0; i < m_queue.Count; i++)
        {
            m_queue[i].Save();
        }

        m_queue.Clear();
        PlayerPrefs.Save();
    }

    public void QueueSave(ISaveable saveable)
    {
        m_queue.Add(saveable);
    }

    public List<T> LoadList<T>(string key, List<T> defaultValue = null)
    {
        int size = Load(key + "_count", -1);
        if (size < 0)
        {
            // Does not exist in PlayerPrefs
            return defaultValue;
        }
        
        List<T> list = new List<T>(size);
        for (int i = 0; i < size; i++)
        {
            T t = Load(key + "_" + i, default(T));
            list.Add(t);
        }

        return list;
    }
    
    public T[] LoadArray<T>(string key, T[] defaultValue = null)
    {
        int size = Load(key + "_count", -1);
        if (size < 0)
        {
            // Does not exist in PlayerPrefs
            return defaultValue;
        }
        
        T[] array = new T[size];
        for (int i = 0; i < size; i++)
        {
            T t = Load(key + "_" + i, default(T));
            array[i] = t;
        }

        return array;
    }
    
    public T Load<T>(string key, T defaultValue)
    {
        if (!PlayerPrefs.HasKey(key))
            return defaultValue;

        if (typeof(T) == typeof(String))
        {
            string s = PlayerPrefs.GetString(key);
            return (T)(object)(s != "-null" ? s : null);
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key);
        }
        else if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key);
        }
        else if (typeof(T) == typeof(double))
        {
            string s = PlayerPrefs.GetString(key);
            return (T)(object)double.Parse(s);
        }
        else if (typeof(T) == typeof(bool))
        {
            int i = PlayerPrefs.GetInt(key);
            return (T)(object)(i == 1);
        }
        else if (typeof(T).IsEnum)
        {
            return (T)(object)PlayerPrefs.GetInt(key);
        }


        throw new Exception("Unknown Type " + typeof(T));
    }
    
    private void Save(string key, object value)
    {
        if (value == null)
        {
            Save(key, "-null");
        }
        else if (value is System.String s1)
        {
            Save(key, s1);
        }
        else if (value is float f1)
        {
            Save(key, f1);
        }
        else if (value is int i1)
        {
            Save(key, i1);
        }
        else if (value is double d1)
        {
            Save(key, d1);
        }
        else if (value is bool b1)
        {
            Save(key, b1);
        }
        else if (value.GetType().IsEnum)
        {
            Save(key, Convert.ToUInt64(value));
        }
        else
        {
            throw new Exception("Unknown Type " + value.GetType());
        }
    }

    public bool KeyExists(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    
    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void Save(string key, double value)
    {
        PlayerPrefs.SetString(key, value.ToString("R"));
    }

    public void Save(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void Save(string key, Enum value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }
    
    public void SaveList<T>(string key, List<T> list)
    {
        if (list == null)
        {
            return;
        }
        
        // Save size
        int size = list.Count;
        Save(key + "_count", size);
        
        // Save each item
        for (int i = 0; i < size; i++)
        {
            Save(key + "_" + i, list[i]);
        }
    }
    
    public void SaveArray<T>(string key, T[] list)
    {
        if (list == null)
        {
            return;
        }
        
        // Save size
        int size = list.Length;
        Save(key + "_count", size);
        
        // Save each item
        for (int i = 0; i < size; i++)
        {
            Save(key + "_" + i, list[i]);
        }
    }
    
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}