﻿using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor.AnimatedValues;
#endif

namespace Hapiga.UI
{
    [Serializable]
    public class PunchData : ScriptableObject
    {
        public string presetCategory = UIAnimatorUtil.UNCATEGORIZED_CATEGORY_NAME;
        public string presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
        public Punch data;
#if UNITY_EDITOR
        public AnimBool isExpanded = new AnimBool(false);
#endif

        public PunchData()
        {
            presetName = UIAnimatorUtil.DEFAULT_PRESET_NAME;
            presetCategory = UIAnimatorUtil.UNCATEGORIZED_CATEGORY_NAME;
            data = new Punch();
#if UNITY_EDITOR
            isExpanded = new AnimBool(false);
#endif
        }
        public bool LoadDefaultValues { get { return presetName.Equals(UIAnimatorUtil.DEFAULT_PRESET_NAME) && presetCategory.Equals(UIAnimatorUtil.UNCATEGORIZED_CATEGORY_NAME); } }
    }
}