using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using System.Collections.Generic;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulationSchedule : SeasonDayTypeSchedule
    {
        private long regulationControl = 0;

        public RegulationSchedule(long globalId) : base(globalId) { }

        public long RegulationControl { get => regulationControl; set => regulationControl = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulationSchedule x = (RegulationSchedule)obj;
                return (x.regulationControl == this.RegulationControl);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    prop.SetValue(regulationControl);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    regulationControl = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulationControl != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL] = new List<long>() { regulationControl };
            }

            base.GetReferences(references, refType);
        }
    }
}
