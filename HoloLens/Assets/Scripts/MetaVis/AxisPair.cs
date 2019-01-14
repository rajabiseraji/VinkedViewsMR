﻿/*
Vinked Views
Copyright(C) 2018  Georg Eckert

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using ETV;
using GraphicalPrimitive;
using System.Collections.Generic;

/// <summary>
/// Pairs which contain the same axis in arbitrary order, are considered equal.
/// </summary>
public class AxisPair
{
    public AAxis A { get; private set; }
    public AAxis B { get; private set; }

    public AxisPair(AAxis a, AAxis b)
    {
        A = a;
        B = b;
    }

    public override bool Equals(object obj)
    {
        if(obj is AxisPair)
        {
            var other = obj as AxisPair;
            return (
                other.A.Equals(A) && other.B.Equals(B)
                ||
                other.A.Equals(B) && other.B.Equals(A));
        } else
            return false;
    }

    public override int GetHashCode()
    {
        var hashCode = -624926263;
        hashCode = hashCode + EqualityComparer<AAxis>.Default.GetHashCode(A);
        hashCode = hashCode + EqualityComparer<AAxis>.Default.GetHashCode(B);
        return hashCode;
    }
}

public class AttributeETVCombination
{
    public string attribute;
    public AETV etv;

    public AttributeETVCombination(string attribute, AETV etv)
    {
        this.attribute = attribute;
        this.etv = etv;
    }

    public override bool Equals(object obj)
    {
        var combination = obj as AttributeETVCombination;
        return combination != null &&
               attribute == combination.attribute &&
               EqualityComparer<AETV>.Default.Equals(etv, combination.etv);
    }

    public override int GetHashCode()
    {
        var hashCode = 61871749;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(attribute);
        hashCode = hashCode * -1521134295 + EqualityComparer<AETV>.Default.GetHashCode(etv);
        return hashCode;
    }
}

public class MetaVisByAttributesAndETV
{
    public AttributeETVCombination comboA;
    public AttributeETVCombination comboB;

    public MetaVisByAttributesAndETV(AxisPair pair)
    {
        comboA = new AttributeETVCombination(pair.A.attributeName, pair.A.Base());
        comboB = new AttributeETVCombination(pair.B.attributeName, pair.B.Base());
    }

    public override bool Equals(object obj)
    {
        var eTV = obj as MetaVisByAttributesAndETV;
        return eTV != null &&
            ((comboA.Equals(eTV.comboA) && comboB.Equals(comboB)) ||
            ((comboA.Equals(eTV.comboB) && comboB.Equals(comboA))));
    }

    public override int GetHashCode()
    {
        var hashCode = -849496519;
        hashCode += comboA.GetHashCode();
        hashCode += comboB.GetHashCode();
        return hashCode;
    }
}