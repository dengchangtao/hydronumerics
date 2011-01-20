#region Copyright

/*
* Copyright (c) HydroInform ApS & Jacob Gudbjerg
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the HydroInform ApS & Jacob Gudbjerg nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "HydroInform ApS & Jacob Gudbjerg" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "HydroInform ApS & Jacob Gudbjerg" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#endregion
using System;
using OpenMI.Standard2;
using OpenMI.Standard2.TimeSpace;

namespace HydroNumerics.OpenMI.Sdk.Backbone
{
    /// <summary>
    /// The exchange item is a combination of a quantity/quality and an element set.
    /// <para>This is a trivial implementation of OpenMI.Standard.IExchangeItem, refer there for further details.</para>
    /// </summary>
    [Serializable]
    public class Input : ExchangeItem, ITimeSpaceInput
    {
        protected ITimeSpaceOutput _provider;
        protected ITimeSpaceValueSet _values;

        public Input(string id)
            : base(id)
        {
        }

        public Input(string id, IValueDefinition valueDefinition, IElementSet elementSet) : 
        	base(id, valueDefinition, elementSet)
        {
        }

        public virtual IBaseOutput Provider
        {
            get { return _provider; }
            set
            {
                _provider = value as ITimeSpaceOutput;
                if (_provider == null)
                    throw new ArgumentException("Provider must be an ITimeSpaceOutput - may need to add an adaptor");
            }
        }

        public virtual IBaseValueSet Values
        {
            get { return _values; }
            set
            {
                if (value != null && !(value is ITimeSpaceValueSet))
                    throw new ArgumentException("Values must be an ITimeSpaceValueSet - you may need to add an adaptor");
                ExchangeItemHelper.CheckValueSizes(this, (ITimeSpaceValueSet)value);
                _values = (ITimeSpaceValueSet)value;
            }
        }

        #region ITimeSpaceInput Members

        ITimeSpaceValueSet ITimeSpaceInput.Values
        {
            get { return (ITimeSpaceValueSet) (Values); }
            set { Values = value; }
        }

        #endregion






    }
}