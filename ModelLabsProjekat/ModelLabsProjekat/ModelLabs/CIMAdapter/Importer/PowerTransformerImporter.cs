using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			///
			ImportDayTypes();
			ImportRegulatingControls();
			ImportRegulationSchedules();
            ImportRegularTimePoints();
            ImportSwitchs();
            ImportTapChangers();
            ImportTapSchedules();






            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        #region Import


        private void ImportRegularTimePoints()
        {
            SortedDictionary<string, object> cim = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cim != null)
            {
                foreach (KeyValuePair<string, object> cimRegularTimePointPair in cim)
                {
                    FTN.RegularTimePoint cimRegularTimePoint = cimRegularTimePointPair.Value as FTN.RegularTimePoint;

                    ResourceDescription rd = CreateRegularTimePointResourceDescription(cimRegularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimRegularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimRegularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

                PowerTransformerConverter.PopulateRegularTimePointsProperties(cimRegularTimePoint, rd, importHelper, report);
            }
            return rd;
        }



        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void ImportDayTypes()
        {
            SortedDictionary<string, object> cimDayTypes = concreteModel.GetAllObjectsOfType("FTN.DayType");
            if (cimDayTypes != null)
            {
                foreach (KeyValuePair<string, object> cimDayTypePair in cimDayTypes)
                {
                    FTN.DayType cimDayType = cimDayTypePair.Value as FTN.DayType;

                    ResourceDescription rd = CreateDayTypeResourceDescription(cimDayType);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("DayType ID = ").Append(cimDayType.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DayType ID = ").Append(cimDayType.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateDayTypeResourceDescription(FTN.DayType cimDayType)
        {
            ResourceDescription rd = null;
            if (cimDayType != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DAYTYPE, importHelper.CheckOutIndexForDMSType(DMSType.DAYTYPE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimDayType.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateDayTypeProperties(cimDayType, rd);
            }
            return rd;
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void ImportRegulatingControls()
        {
            SortedDictionary<string, object> cimRegulatingControls = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
            if (cimRegulatingControls != null)
            {
                foreach (KeyValuePair<string, object> cimTRegulatingControlPair in cimRegulatingControls)
                {
                    FTN.RegulatingControl cimRegulatingControl = cimTRegulatingControlPair.Value as FTN.RegulatingControl;

                    ResourceDescription rd = CreateRegulatingControlResourceDescription(cimRegulatingControl);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegulatingControlResourceDescription(FTN.RegulatingControl cimRegulatingControl)
        {
            ResourceDescription rd = null;
            if (cimRegulatingControl != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATINGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGULATINGCONTROL));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegulatingControl.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateRegulatingControlProperties(cimRegulatingControl, rd);
            }
            return rd;

        }


        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>


        private void ImportRegulationSchedules()
        {
            SortedDictionary<string, object> cimRegulationSchedules = concreteModel.GetAllObjectsOfType("FTN.RegulationSchedule");
            if (cimRegulationSchedules != null)
            {
                foreach (KeyValuePair<string, object> RegulationSchedulePair in cimRegulationSchedules)
                {
                    FTN.RegulationSchedule cimRegulationSchedule = RegulationSchedulePair.Value as FTN.RegulationSchedule;

                    ResourceDescription rd = CreateRegulationScheduleResourceDescription(cimRegulationSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegulationScheduleResourceDescription(FTN.RegulationSchedule cimRegulationSchedule)
        {
            ResourceDescription rd = null;
            if (cimRegulationSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATIONSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULATIONSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegulationSchedule.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateRegulationScheduleProperties(cimRegulationSchedule, rd, importHelper, report);
            }
            return rd;
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void ImportSwitchs()
        {
            SortedDictionary<string, object> cimSwitches = concreteModel.GetAllObjectsOfType("FTN.Switch");
            if (cimSwitches != null)
            {
                foreach (KeyValuePair<string, object> SwitchesPair in cimSwitches)
                {
                    FTN.Switch cimSwitch = SwitchesPair.Value as FTN.Switch;

                    ResourceDescription rd = CreateSwitchResourceDescription(cimSwitch);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Switch ID = ").Append(cimSwitch.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Switch ID = ").Append(cimSwitch.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSwitchResourceDescription(FTN.Switch cimSwitch)
        {
            ResourceDescription rd = null;
            if (cimSwitch != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCH, importHelper.CheckOutIndexForDMSType(DMSType.SWITCH));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSwitch.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateSwitchProperties(cimSwitch, rd, importHelper, report);
            }
            return rd;
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void ImportTapChangers()
        {
            SortedDictionary<string, object> cimTapChangers = concreteModel.GetAllObjectsOfType("FTN.TapChanger");
            if (cimTapChangers != null)
            {
                foreach (KeyValuePair<string, object> TapChangerPair in cimTapChangers)
                {
                    FTN.TapChanger cimTapChanger = TapChangerPair.Value as FTN.TapChanger;

                    ResourceDescription rd = CreateTapChangerResourceDescription(cimTapChanger);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Tap Changer ID = ").Append(cimTapChanger.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Tap Changer ID = ").Append(cimTapChanger.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTapChangerResourceDescription(FTN.TapChanger cimTapChanger)
        {
            ResourceDescription rd = null;
            if (cimTapChanger != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPCHANGER, importHelper.CheckOutIndexForDMSType(DMSType.TAPCHANGER));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTapChanger.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateTapChangerProperties(cimTapChanger, rd, importHelper, report);
            }
            return rd;
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>

        private void ImportTapSchedules()
        {
            SortedDictionary<string, object> cimTapSchedules = concreteModel.GetAllObjectsOfType("FTN.TapSchedule");
            if (cimTapSchedules != null)
            {
                foreach (KeyValuePair<string, object> TapSchedulePair in cimTapSchedules)
                {
                    FTN.TapSchedule cimTapSchedule = TapSchedulePair.Value as FTN.TapSchedule;

                    ResourceDescription rd = CreateTapScheduleResourceDescription(cimTapSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("TapSchedule ID = ").Append(cimTapSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("TapSchedule ID = ").Append(cimTapSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTapScheduleResourceDescription(FTN.TapSchedule cimTapSchedule)
        {
            ResourceDescription rd = null;
            if (cimTapSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TAPSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.TAPSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTapSchedule.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateTapScheduleProperties(cimTapSchedule, rd, importHelper, report);
            }
            return rd;
        }








        #endregion Import
    }
}

