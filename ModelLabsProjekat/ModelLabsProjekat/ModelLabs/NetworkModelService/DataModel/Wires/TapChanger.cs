using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapChanger : PowerSystemResource
    {
        private long highStep;
        private float initialDelay;
        private long lowStep;
        private bool ltcFlag;
        private long neutralStep;
        private float neutralU;
        private long normalStep;
        private bool regulationStatus;
        private long subsequentDelay;
        private List<long> tapSchedule = new List<long>();

        public long HighStep { get => highStep; set => highStep = value; }
        public float InitialDelay { get => initialDelay; set => initialDelay = value; }
        public long LowStep { get => lowStep; set => lowStep = value; }
        public long NeutralStep { get => neutralStep; set => neutralStep = value; }
        public bool LtcFlag { get => ltcFlag; set => ltcFlag = value; }
        public float NeutralU { get => neutralU; set => neutralU = value; }
        public long NormalStep { get => normalStep; set => normalStep = value; }
        public bool RegulationStatus { get => regulationStatus; set => regulationStatus = value; }
        public long SubsequentDelay { get => subsequentDelay; set => subsequentDelay = value; }
        public List<long> TapSchedule { get => tapSchedule; set => tapSchedule = value; }

        public TapChanger(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChanger x = (TapChanger)obj;
                return (x.highStep == this.HighStep &&
                    x.lowStep == this.LowStep &&
                    x.neutralStep == this.NeutralStep && x.ltcFlag == this.LtcFlag && x.neutralU == this.NeutralU && x.normalStep == this.NormalStep &&
                    x.regulationStatus == this.RegulationStatus && (CompareHelper.CompareLists(x.tapSchedule, this.TapSchedule)));
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
                case ModelCode.TAPCHANGER_HIGHSTEP:
                //case ModelCode.TAPCHANGER_INITIALDELAY:
                case ModelCode.TAPCHANGER_ITCFLAG:
                case ModelCode.TAPCHANGER_LOWSTEP:
                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                case ModelCode.TAPCHANGER_NEUTRALU:
                case ModelCode.TAPCHANGER_NORMALSTEP:
                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                //case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                case ModelCode.TAPCHANGER_TAPSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    prop.SetValue(highStep);
                    break;
                //case ModelCode.TAPCHANGER_INITIALDELAY:
                //    prop.SetValue(initialDelay);
                //    break;
                case ModelCode.TAPCHANGER_ITCFLAG:
                    prop.SetValue(ltcFlag);
                    break;
                case ModelCode.TAPCHANGER_LOWSTEP:
                    prop.SetValue(lowStep);
                    break;
                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                    prop.SetValue(neutralStep);
                    break;
                case ModelCode.TAPCHANGER_NEUTRALU:
                    prop.SetValue(neutralU);
                    break;
                case ModelCode.TAPCHANGER_NORMALSTEP:
                    prop.SetValue(normalStep);
                    break;
                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                    prop.SetValue(regulationStatus);
                    break;
                //case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                //    prop.SetValue(subsequentDelay);
                //    break;
                case ModelCode.TAPCHANGER_TAPSCHEDULE:
                    prop.SetValue(TapSchedule);
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
                case ModelCode.TAPCHANGER_HIGHSTEP:
                    highStep = property.AsLong();
                    break;

                //case ModelCode.TAPCHANGER_INITIALDELAY:
                //    initialDelay = property.AsLong();
                //    break;

                case ModelCode.TAPCHANGER_ITCFLAG:
                    ltcFlag = property.AsBool();
                    break;

                case ModelCode.TAPCHANGER_LOWSTEP:
                    lowStep = property.AsLong();
                    break;

                case ModelCode.TAPCHANGER_NEUTRALSTEP:
                    neutralStep = property.AsLong();
                    break;

                case ModelCode.TAPCHANGER_NEUTRALU:
                    neutralU = property.AsFloat();
                    break;

                case ModelCode.TAPCHANGER_NORMALSTEP:
                    normalStep = property.AsLong();
                    break;

                case ModelCode.TAPCHANGER_REGULATIONSTATUS:
                    regulationStatus = property.AsBool();
                    break;

                //case ModelCode.TAPCHANGER_SUBSEQUENTDELAY:
                //    subsequentDelay = property.AsLong();
                //    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        public override bool IsReferenced
        {
            get
            {
                return tapSchedule.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (tapSchedule != null && tapSchedule.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGER_TAPSCHEDULE] = tapSchedule.GetRange(0, tapSchedule.Count);
            }
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPSCHEDULE_TAPCHANGER:
                    tapSchedule.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPSCHEDULE_TAPCHANGER:

                    if (tapSchedule.Contains(globalId))
                    {
                        tapSchedule.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }
                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }

        }
    }

}