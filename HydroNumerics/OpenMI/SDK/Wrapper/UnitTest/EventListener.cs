#region Copyright
/*
* Copyright (c) 2005,2006,2007, HydroNumerics
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the HydroNumerics nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "HydroNumerics" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "HydroNumerics" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion 
using System;
using System.Collections;
using System.IO;
using OpenMI.Standard;
using HydroNumerics.OpenMI.Sdk.Backbone;
using HydroNumerics.OpenMI.Sdk.DevelopmentSupport;

namespace HydroNumerics.OpenMI.Sdk.Wrapper.UnitTest
{
	/// <summary>
	/// Summary description for EventListener.
	/// </summary>
	public class EventListener   	: IListener
	{
		private ArrayList _acceptenEventTypes;
		public bool _isSilent;

		public bool _dataChanged;
		public bool _globalProgress;
		public bool _informative;
		public bool _other;
		public bool _sourceAfterGetValuesCall;
		public bool _sourceBeforeGetValuesReturn;
		public bool _targetAfterGetValuesReturn;
		public bool _targetBeforeGetValuesCall;
		public bool _timeStepProgres;
		public bool _valueOutOfRange;
		public bool _warning;
		
		
		public EventListener()
		{
			_isSilent = false;
			_acceptenEventTypes = new ArrayList();
			_acceptenEventTypes.Add(EventType.DataChanged);
			_acceptenEventTypes.Add(EventType.GlobalProgress);
			_acceptenEventTypes.Add(EventType.Informative);
			_acceptenEventTypes.Add(EventType.Other);
			_acceptenEventTypes.Add(EventType.SourceAfterGetValuesCall);
			_acceptenEventTypes.Add(EventType.SourceBeforeGetValuesReturn);
			_acceptenEventTypes.Add(EventType.TargetAfterGetValuesReturn);
			_acceptenEventTypes.Add(EventType.TargetBeforeGetValuesCall);
			_acceptenEventTypes.Add(EventType.TimeStepProgres);
			_acceptenEventTypes.Add(EventType.ValueOutOfRange);
			_acceptenEventTypes.Add(EventType.Warning);

			_dataChanged					= false;
			_globalProgress					= false;
			_informative					= false;
			_other							= false;
			_sourceAfterGetValuesCall		= false;
			_sourceBeforeGetValuesReturn	= false;
			_targetAfterGetValuesReturn		= false;
			_targetBeforeGetValuesCall		= false;
			_timeStepProgres				= false;
			_valueOutOfRange				= false;
			_warning						= false;

		}
		#region IListener Members

		public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
		{
			return (EventType) _acceptenEventTypes[acceptedEventTypeIndex];

		}

		public int GetAcceptedEventTypeCount()
		{
			return _acceptenEventTypes.Count;
		}

		public void OnEvent(IEvent Event)
		{

			if (Event.Type == EventType.DataChanged)
			{
				_dataChanged					= true;
			}
			if (Event.Type == EventType.GlobalProgress)
			{
				_globalProgress					= true;
			}
			if (Event.Type == EventType.Informative)
			{
				_informative					= true;
			}
			if (Event.Type == EventType.Other)
			{
				_other							= true;
			}
			if (Event.Type == EventType.SourceAfterGetValuesCall)
			{
				_sourceAfterGetValuesCall		= true;
			}
			if (Event.Type == EventType.SourceBeforeGetValuesReturn)
			{
				_sourceBeforeGetValuesReturn	= true;
			}
			if (Event.Type == EventType.TargetAfterGetValuesReturn)
			{
				_targetAfterGetValuesReturn		= true;
			}
			if (Event.Type == EventType.TargetBeforeGetValuesCall)
			{
				_targetBeforeGetValuesCall		= true;
			}
			if (Event.Type == EventType.TimeStepProgres)
			{
				_timeStepProgres				= true;
			}
			if (Event.Type == EventType.ValueOutOfRange)
			{
				_valueOutOfRange				= true;
			}
			if (Event.Type == EventType.Warning)
			{
				_warning						= true;
			}

			
			
			if (Event.Type == EventType.DataChanged)
			{
				if (!_isSilent)
				{
					Console.WriteLine(" ");
					Console.WriteLine("   Event Type      : " + Event.Type.ToString());
					Console.WriteLine("   Event Message   : " + Event.Description);
					Console.WriteLine("   Component ID    : " + ((ILinkableComponent) Event.Sender).ComponentID);
					Console.WriteLine("   Simulation time : " + CalendarConverter.ModifiedJulian2Gregorian(Event.SimulationTime.ModifiedJulianDay).ToString());
					Console.WriteLine(" ");
				}
			}
			else
			{
				if (!_isSilent)
				{
					Console.WriteLine(" ");
				
					Console.WriteLine("Event Type      : " + Event.Type.ToString());
					Console.WriteLine("Event Message   : " + Event.Description);
					Console.WriteLine("Component ID    : " + ((ILinkableComponent) Event.Sender).ComponentID);
					Console.WriteLine("Simulation time : " + CalendarConverter.ModifiedJulian2Gregorian(Event.SimulationTime.ModifiedJulianDay).ToString());
					Console.WriteLine(" ");
				}
			}
		}

		#endregion
	}
}
