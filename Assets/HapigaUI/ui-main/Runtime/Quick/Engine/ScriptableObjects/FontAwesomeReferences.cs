﻿using QuickEngine.Core;
using UnityEngine;

namespace QuickEngine.Utils
{
    public class FontAwesomeReferences : ScriptableObject
    {
        public const string RESOURCES_PATH = "Quick/Fonts/FontAwesome/";
        public static string RELATIVE_PATH { get { return Q.PATH + "/Resources/" + RESOURCES_PATH; } }


        private static FontAwesomeReferences _instance;
        public static FontAwesomeReferences Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = Q.GetResource<FontAwesomeReferences>(RESOURCES_PATH, "FontAwesomeReferences");

#if UNITY_EDITOR
                    if(_instance == null)
                    {
                        _instance = Q.CreateAsset<FontAwesomeReferences>(RELATIVE_PATH, "FontAwesomeReferences");
                    }
#endif
                }
                return _instance;
            }
        }

        public Font FontAwesomeBrands;

        [Header("FREE")]
        public Font FontAwesomeSolid;
        public Font FontAwesomeRegular;

        [Header("PRO")]
        public Font FontAwesomeSolidPro;
        public Font FontAwesomeRegularPro;
        public Font FontAwesomeLightPro;
    }
}
