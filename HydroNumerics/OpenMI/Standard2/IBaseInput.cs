#region Copyright
/*
    Copyright (c) 2005-2010, OpenMI Association
    "http://www.openmi.org/"

    This file is part of OpenMI.Standard2.dll

    OpenMI.Standard2.dll is free software; you can redistribute it and/or modify
    it under the terms of the Lesser GNU General Public License as published by
    the Free Software Foundation; either version 3 of the License, or
    (at your option) any later version.

    OpenMI.Standard2.dll is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    Lesser GNU General Public License for more details.

    You should have received a copy of the Lesser GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

namespace OpenMI.Standard2
{
    /// <summary>
    /// <para>
    /// An input item that can accept values for an <see cref="IBaseLinkableComponent"/>.
    /// </para>
    /// </summary>
    public interface IBaseInput : IBaseExchangeItem
    {
        ///<summary>
        /// The provider this input should get its values from.
        ///</summary>
        IBaseOutput Provider { get; set; }

        ///<summary>
        /// The exchange item's values.
        ///</summary>
        IBaseValueSet Values { get; set; }
    }
}

