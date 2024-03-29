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
* THIS SOFTWARE IS PROVCaptionED BY "HydroInform ApS & Jacob Gudbjerg" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "HydroInform ApS & Jacob Gudbjerg" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCCaptionENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#endregion

using System;
using System.Collections.Generic;
using OpenMI.Standard2;
using OpenMI.Standard2.TimeSpace;

namespace HydroNumerics.OpenMI.Sdk.Backbone
{
    /// <summary>
    /// The <see cref="AbstractAdaptedOutput"/> class contains operations the providing component should
    /// carry out on the data.
    /// <para>This is a trivial implementation of OpenMI.Standard.CaptionataOperation, refer there for further details.</para>
    /// </summary>
    [Serializable]
    public abstract class AbstractAdaptedOutput : Output, ITimeSpaceAdaptedOutput
    {
        #region Private/protected fields

        private readonly List<IArgument> _arguments = new List<IArgument>();
        protected ITimeSpaceOutput _adaptee;

        #endregion

        #region Constructors

        protected AbstractAdaptedOutput(string id) : base(id)
        {
        }

        protected AbstractAdaptedOutput(string id, ITimeSpaceOutput decoratedOutput)
            : base(id)
        {
            _adaptee = decoratedOutput;
        }


        #endregion

        #region Overridden IOutput Members

        public abstract override ITimeSpaceValueSet GetValues(IBaseExchangeItem querySpecifier);

        #endregion

        #region IAdaptedOutput Members

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public IList<IArgument> Arguments
        {
            get { return _arguments; }
        }

        public virtual IBaseOutput Adaptee
        {
            get { return _adaptee; }
        }

        public override IBaseLinkableComponent Component
        {
            get
            {
              if (_adaptee != null)
                return _adaptee.Component;
              return (base.Component);
            }
        }

        public abstract void Refresh();

        #endregion
        

    }
}