﻿using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel;

public class AbilityData : BaseData
{
    public object[] Speciality { get; set; } = new string[0];
}
