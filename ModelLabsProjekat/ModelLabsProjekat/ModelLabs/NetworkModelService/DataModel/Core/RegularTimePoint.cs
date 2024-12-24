using FTN.Common;
using System.Collections.Generic;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private long sequenceNumber;
        private float value1;
        private float value2;
        private long intervalSchedule = 0;

        public RegularTimePoint(long globalId) : base(globalId) { }

        public long SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularTimePoint x = (RegularTimePoint)obj;
                return (x.sequenceNumber == this.SequenceNumber &&
                    x.value1 == this.Value1 && x.value2 == this.Value2 &&
                    x.intervalSchedule == this.IntervalSchedule);
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
                case ModelCode.RTP_SEQUENCENUMBER:
                case ModelCode.RTP_VALUE1:
                case ModelCode.RTP_VALUE2:
                case ModelCode.RTP_INTERVALSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.RTP_SEQUENCENUMBER:
                    prop.SetValue(sequenceNumber);
                    break;
                case ModelCode.RTP_VALUE1:
                    prop.SetValue(value1);
                    break;
                case ModelCode.RTP_VALUE2:
                    prop.SetValue(value2);
                    break;
                case ModelCode.RTP_INTERVALSCHEDULE:
                    prop.SetValue(intervalSchedule);
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
                case ModelCode.RTP_SEQUENCENUMBER:
                    sequenceNumber = property.AsLong();
                    break;

                case ModelCode.RTP_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.RTP_VALUE2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.RTP_INTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalSchedule != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.RTP_INTERVALSCHEDULE] = new List<long> { intervalSchedule };
            }

            base.GetReferences(references, refType);
        }
    }
}
