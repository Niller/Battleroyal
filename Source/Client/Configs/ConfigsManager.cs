using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Configs.View;
using UnityEngine;

namespace Client.Configs
{
    class ConfigsManager : MonoBehaviour
    {
        public static ConfigsManager Instance;

#pragma warning disable 649

        public ViewConfig ViewConfig;

#pragma warning restore 649

        private void Awake()
        {
            Instance = this;
        }
    }
}
