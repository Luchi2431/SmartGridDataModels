using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		DAYTYPE							    = 0x0001,
		REGULARTIMEPOINT					= 0x0002,
		REGULATIONSCHEDULE					= 0x0003,
		SWITCH								= 0x0004,
		TAPCHANGER							= 0x0005,
		TAPSCHEDULE							= 0x0006,
		REGULATINGCONTROL					= 0X0007,
	}

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								= 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
		IDOBJ_ALIASNAME						= 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,

        BASICINTSCH							= 0x1100000000000000,
        BASICINTSCH_STIME					= 0x1100000000000108,
        BASICINTSCH_VAL1_MUL			    = 0x110000000000020a,
        BASICINTSCH_VAL1_UNT				= 0x110000000000030a,
        BASICINTSCH_VAL2_MUL				= 0x110000000000040a,
        BASICINTSCH_VAL2_UNT				= 0x110000000000050a,

        DAYTYPE								= 0x1200000000010000,//TU NEMA NISTA
		DAYTYPE_SDTS						= 0x1200000000010119, //veza

		RTP									= 0x1300000000020000, //REGULARTIMEPOINT
		RTP_SEQUENCENUMBER  				= 0x1300000000020104, //INTEGER
		RTP_VALUE1							= 0x1300000000020205, //FLOAT
		RTP_VALUE2							= 0x1300000000020305, 
		RTP_INTERVALSCHEDULE				= 0x1300000000020409,

		PSR									= 0x1400000000000000, //POWERSISTEMRESOURCE

		RIS									= 0x1110000000000000, //REGULARINTERVALSCHEDULE
		RIS_ENDTIME							= 0x1110000000000108,
		//RIS_TIMESTEP						= 0x1110000000000204,
		RIS_TIMEPOINT						= 0x1110000000000219, //VEZA 1...N
																  	

		EQUIPMENT							= 0x1410000000000000,
		EQUIPMENT_AGGREGATE					= 0x1410000000000101, //BOOLEAN
		EQUIPMENT_NORMALLYINSERVICE			= 0x1410000000000201,

		RC									= 0x1420000000070000, //REGULATINGCONTROL
		RC_DISCRETE							= 0x1420000000070101,
		RC_MODE								= 0x142000000007020a, //ENUM
		RC_MONITOREDPHASE				    = 0x142000000007030a,
		RC_TARGETRANGE						= 0x1420000000070405,
		RC_TARGETVALUE					    = 0x1420000000070505,
		RC_REGULATIONSCHEDULE				= 0x1420000000070619, //veza

		TAPCHANGER							= 0x1430000000050000,
        TAPCHANGER_HIGHSTEP					= 0x1430000000050104,
        //TAPCHANGER_INITIALDELAY				= 0x1430000000050204,
        TAPCHANGER_LOWSTEP					= 0x1430000000050204,
        TAPCHANGER_ITCFLAG					= 0x1430000000050301,
        TAPCHANGER_NEUTRALSTEP				= 0x1430000000050404,
        TAPCHANGER_NEUTRALU					= 0x1430000000050505,
        TAPCHANGER_NORMALSTEP			    = 0x1430000000050604,
        TAPCHANGER_REGULATIONSTATUS         = 0x1430000000050701,
        //TAPCHANGER_SUBSEQUENTDELAY			= 0x1430000000050904,
        TAPCHANGER_TAPSCHEDULE				= 0x1430000000050819, //veza

		SDTS								= 0x1111000000000000, //SEASONDAYTYPESCHEDULE
		SDTS_DAYTYPE						= 0x1111000000000109, //veza

		CONDUCTINGEQUIPMENT					= 0x1411000000000000,

		TAPSCHEDULE							= 0x1111100000060000,
		TAPSCHEDULE_TAPCHANGER		        = 0x1111100000060109, //veza

		REGULATIONSCHEDULE				    = 0x1111200000030000,
		REGULATIONSCHEDULE_REGULATINGCONTROL= 0x1111200000030109, //veza

        SWITCH                              = 0x1411100000040000,
        SWITCH_NORMAL_OPEN					= 0x1411100000040101,
        SWITCH_RATCURRENT					= 0x1411100000040205,
        SWITCH_RETAINED						= 0x1411100000040301,
        SWITCH_S_ON_COUNT					= 0x1411100000040404,
        SWITCH_S_ON_DATE					= 0x1411100000040508,  

    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}

