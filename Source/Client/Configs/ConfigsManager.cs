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

        public ViewConfig ViewConfig;

        private void Awake()
        {
            Instance = this;
        }
    }
}
