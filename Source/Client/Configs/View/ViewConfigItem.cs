using System;
using UnityEngine;

namespace Client.Configs.View
{
    [Serializable]
    public struct ViewConfigItem : IConfigItem<ViewType, GameObject>
    {
        public ViewType Type;
        public GameObject GameObject;
        public ViewType GetKey()
        {
            return Type;
        }

        public GameObject GetValue()
        {
            return GameObject;
        }
    }
}