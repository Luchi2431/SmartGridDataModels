using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapSchedule : SeasonDayTypeSchedule
    {
        private long tapChanger = 0;

        public TapSchedule(long globalId) : base(globalId)
        {
        }

        public long TapChanger { get => tapChanger; set => tapChanger = value; }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapSchedule x = (TapSchedule)obj;
                return x.tapChanger == this.TapChanger;
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
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    prop.SetValue(tapChanger);
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
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    tapChanger = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (tapChanger != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.TAPSCHEDULE_TAPCHANGER] = new List<long> { tapChanger };
            }

            base.GetReferences(references, refType);
        }
    }
    
}
