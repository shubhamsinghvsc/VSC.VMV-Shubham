﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.VMV.Common.Models.Base;

namespace VSC.VMV.Common.Models.Frontdesk
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Frontdesk : PeopleMetaData
    {

    }
}