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
﻿using OpenMI.Standard2.TimeSpace;

namespace HydroNumerics.OpenMI.Sdk.Spatial
{
  /// <summary>
  /// An <see cref="ITimeSpaceAdaptedOutput"/> that wraps the 
  /// <see cref="ISpatialDefinition"/> of the adaptee into
  /// the matching <see cref="IElementSet"/> (provided in the constructor)
  /// </summary>
  public class SpatialExtensionElementSetAdaptor : AbstractTimeSpaceAdaptor
  {
    private readonly IElementSet _elementSet;
    private string _caption;
    private string _description;

    public SpatialExtensionElementSetAdaptor(ITimeSpaceOutput adaptee, IElementSet elementSet, string id)
      : base(adaptee, id)
    {
      _elementSet = elementSet;
      _caption = elementSet.Caption;
      _description = elementSet.Description;
    }

    public override string Caption
    {
      get
      {
        return (_caption);
      }
      set
      {
        _caption = value;
      }
    }

    public override string Description
    {
      get
      {
        return (_description);
      }
      set
      {
        _description = value;
      }
    }
    public override ISpatialDefinition SpatialDefinition
    {
      get { return _elementSet; }
    }
  }
}
