using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace PostProcessing.Profiles
{
    public class VolumeProfileLibrary : SingletonBehaviour<VolumeProfileLibrary>
    {
        //--------------------Private--------------------//
        [SerializeField]
        private List<VolumeProfile> _volumeProfiles = new();

        //--------------------Public--------------------//
        public List<VolumeProfile> VolumeProfiles => _volumeProfiles;
    }
}