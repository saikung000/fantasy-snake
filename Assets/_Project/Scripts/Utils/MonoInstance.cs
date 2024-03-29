﻿using UnityEngine;

public class MonoInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogWarning("[MonoInstance] Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it.");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(MonoInstance) " + typeof(T);
                    }
                }

                return instance;
            }
        }
    }

    public void Ping()
    {
        //Debug.Log("[MonoInstance]: `" + this + "` is alive!");
    }

    #region override method
    protected virtual void Awake()
    {
        Ping();
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }
    #endregion
}