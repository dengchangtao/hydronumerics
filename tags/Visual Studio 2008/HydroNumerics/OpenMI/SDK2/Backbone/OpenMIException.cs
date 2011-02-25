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
﻿using System;
using OpenMI.Standard2;

namespace HydroNumerics.OpenMI.Sdk.Backbone
{
    /// <summary>
    /// The OpenMI Exception extends the .Net exception in such a way that the involved linkable component and/or
    /// input and/or output can be specified.
    /// </summary>
    public class OpenMIException : Exception
    {
        #region Constructors

        /// <summary>
        /// Create an OpenMI exception around an already thrown exception, or around a specific type of exception.
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public OpenMIException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// Create a regular OpenMI exception.
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public OpenMIException(String text)
            : base(text, new Exception(text))
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The linkable component that has thrown the exception. If the value is <code>null</code>, there was no specific component involved.
        /// </summary>
        public IBaseLinkableComponent Component { get; set; }

        /// <summary>
        /// The output item that was involved when the exception was thrown. If the value is <code>null</code>, there was no specific output item involved.
        /// </summary>
        public IBaseOutput Output { get; set; }

        /// <summary>
        /// The input item that was involved when the exception was thrown. If the value is <code>null</code>, there was no specific output item involved.
        /// </summary>
        public IBaseInput Input { get; set; }

        #endregion Properties
    }
}