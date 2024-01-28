using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using VladStorm;

public class EffectsController : Singleton<EffectsController>
{
    public PostProcessVolume volume;
    private void Awake()
    {
        if (volume.profile.TryGetSettings(out VHSPro vhsPro))
        {

            vhsPro.tapeNoiseOn.value = true;
            vhsPro.colorMode.value = 2;
        }
    }
}
