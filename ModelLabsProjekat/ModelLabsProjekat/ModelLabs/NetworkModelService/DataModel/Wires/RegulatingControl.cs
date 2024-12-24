using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System.Collections.Generic;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private List<long> regulationSchedules = new List<long>();

        public RegulatingControl(long globalId) : base(globalId) { }

        public bool Discrete { get => discrete; set => discrete = value; }
        public RegulatingControlModeKind Mode { get => mode; set => mode = value; }
        public PhaseCode MonitoredPhase { get => monitoredPhase; set => monitoredPhase = value; }
        public float TargetRange { get => targetRange; set => targetRange = value; }
        public float TargetValue { get => targetValue; set => targetValue = value; }
        public List<long> RegulationSchedules { get => regulationSchedules; set => regulationSchedules = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return ((x.discrete == this.Discrete) && x.mode == this.Mode && x.monitoredPhase == this.MonitoredPhase
                    && x.targetValue == this.targetValue && x.TargetRange == this.targetRange &&
                        (CompareHelper.CompareLists(x.regulationSchedules, this.regulationSchedules)));
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
                case ModelCode.RC_DISCRETE:
                case ModelCode.RC_MODE:
                case ModelCode.RC_MONITOREDPHASE:
                case ModelCode.RC_TARGETRANGE:
                case ModelCode.RC_TARGETVALUE:
                case ModelCode.RC_REGULATIONSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.RC_DISCRETE:
                    prop.SetValue(discrete);
                    break;
                case ModelCode.RC_MODE:
                    prop.SetValue((short)mode);
                    break;
                case ModelCode.RC_MONITOREDPHASE:
                    prop.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.RC_TARGETRANGE:
                    prop.SetValue(targetRange);
                    break;
                case ModelCode.RC_TARGETVALUE:
                    prop.SetValue(targetValue);
                    break;
                case ModelCode.RC_REGULATIONSCHEDULE:
                    prop.SetValue(regulationSchedules);
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
                case ModelCode.RC_DISCRETE:
                    discrete = property.AsBool();
                    break;

                case ModelCode.RC_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;

                case ModelCode.RC_MONITOREDPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;

                case ModelCode.RC_TARGETRANGE:
                    targetRange = property.AsFloat();
                    break;

                case ModelCode.RC_TARGETVALUE:
                    targetValue = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return regulationSchedules.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulationSchedules != null && regulationSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.RC_REGULATIONSCHEDULE] = regulationSchedules.GetRange(0, regulationSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:
                    regulationSchedules.Add(globalId);
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
                case ModelCode.REGULATIONSCHEDULE_REGULATINGCONTROL:

                    if (regulationSchedules.Contains(globalId))
                    {
                        regulationSchedules.Remove(globalId);
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
