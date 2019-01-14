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

using System.Collections.Generic;
using System.Linq;

namespace Model
{
    /// <summary>
    /// Holds information about level of measurement and name of an attribute
    /// </summary>
    public class AttributeStats
    {
        public LoM type;
        public string name;

        public AttributeStats(LoM type, string name)
        {
            this.type = type;
            this.name = name;
        }
    }

    /// <summary>
    /// Holds information about:
    ///     * level of measurement
    ///     * name
    ///     * value distribution
    ///     * dictionary for value - ID translation
    ///     * minimum, maximum and range of distribution
    ///     * zero bound minimum, maximum and range of distribution
    ///     * minimum, maximum and range of IDs
    ///     * number of unique values
    /// </summary>
    public class NominalAttributeStats : AttributeStats
    {
        public IDictionary<string, int> distribution;
        public string[] uniqueValues;
        public IDictionary<string, int> valueIDs;
        public IDictionary<int, string> idValues;
        public int distMin, zBoundDistMin;
        public int distMax, zBoundDistMax;
        public int distRange, zBoundDistRange;
        public int min, max, range;
        public int numberOfUniqueValues;

        public NominalAttributeStats(IDictionary<string, int> distribution, string variableName)
             : base(LoM.NOMINAL, variableName)
        {
            this.distribution = distribution;
            valueIDs = new Dictionary<string, int>();
            idValues = new Dictionary<int, string>();

            this.distMin = distribution.Values.Min();
            this.distMax = distribution.Values.Max();
            this.zBoundDistMin = 0;
            this.zBoundDistMax = (distMax == 0) ? 0 : distMax;
            this.distRange = distMax - distMin;
            this.zBoundDistRange = zBoundDistMax - zBoundDistMin;
            this.numberOfUniqueValues = distribution.Keys.Count;
            this.uniqueValues = new string[distribution.Keys.Count];
            this.min = 0;
            this.max = numberOfUniqueValues-1;
            this.range = numberOfUniqueValues-1;

            int counter = 0;
            foreach(string key in distribution.Keys)
            {
                valueIDs.Add(key, counter);
                idValues.Add(counter, key);
                uniqueValues[counter] = key;
                counter++;
            }
        }

        public int[] GetDistributionValues()
        {
            int[] vals = new int[uniqueValues.Length];
            for(int i=0; i < uniqueValues.Length; i++)
            {
                vals[i] = distribution[uniqueValues[i]];
            }

            return vals;
        }
    }

    /// <summary>
    /// Holds information about:
    ///     * level of measurement
    ///     * name
    ///     * value distribution
    ///     * dictionary for value - ID translation
    ///     * minimum, maximum and range of distribution
    ///     * zero bound minimum, maximum and range of distribution
    ///     * minimum, maximum and range of IDs
    ///     * number of unique values
    ///     * order of the ordinal values
    /// </summary>
    public class OrdinalAttributeStats : AttributeStats
    {
        public IDictionary<int, int> distribution;
        public string[] uniqueValues;
        public int[] uniqueIDs;
        public IDictionary<int, string> orderedValueIDs;
        public IDictionary<string, int> orderedIDValues;
        public int distMin, zBoundDistMin;
        public int distMax, zBoundDistMax;
        public int distRange, zBoundDistRange;
        public int min, max, range;
        public int numberOfUniqueValues;

        public OrdinalAttributeStats(IDictionary<int, string> orderedValueIDs, IDictionary<int, int> distribution, string variableName)
             : base(LoM.ORDINAL, variableName)
        {
            this.orderedValueIDs = orderedValueIDs;
            this.distribution = distribution;

            uniqueValues = new string[orderedValueIDs.Keys.Count];
            uniqueIDs = new int[orderedValueIDs.Keys.Count];
            int counter = 0;
            foreach(int key in orderedValueIDs.Keys)
            {
                uniqueValues[counter] = orderedValueIDs[key];
                uniqueIDs[counter] = key;
                counter++;
            }

            orderedIDValues = new Dictionary<string, int>();
            foreach(var key in orderedValueIDs.Keys)
            {
                var name = orderedValueIDs[key];
                orderedIDValues.Add(name, key);
            }

            this.distMin = distribution.Values.Min();
            this.distMax = distribution.Values.Max();
            this.zBoundDistMin = 0;
            this.zBoundDistMax = (distMax == 0) ? 0 : distMax;
            this.distRange = distMax - distMin;
            this.zBoundDistRange = zBoundDistMax - zBoundDistMin;
            this.numberOfUniqueValues = uniqueValues.Length;

            this.min = 0;
            this.max = orderedValueIDs.Keys.Max();
            this.range = numberOfUniqueValues;
        }

        public float NormalizeToRange(int value)
        {
            return (((float)value) / range);
        }

        public int[] GetDistributionValues()
        {
            int[] vals = new int[uniqueValues.Length];
            for(int i = 0; i < uniqueValues.Length; i++)
            {
                vals[i] = distribution[uniqueIDs[i]];
            }

            return vals;
        }
    }

    /// <summary>
    /// Holds information about:
    ///     * level of measurement
    ///     * name
    ///     * value distribution
    ///     * dictionary for value - ID translation
    ///     * minimum, maximum and range of distribution
    ///     * zero bound minimum, maximum and range of distribution
    ///     * minimum, maximum and range of IDs
    ///     * number of unique values
    ///     * order of the ordinal values
    /// </summary>
    public class IntervalAttributeStats : AttributeStats
    {
        public IDictionary<int, int> distribution;
        public int distMin, zBoundDistMin;
        public int distMax, zBoundDistMax;
        public int distRange, zBoundDistRange;
        public int min, max, range;
        public string intervalTranslator;

        public IntervalAttributeStats(IDictionary<int, int> distribution, string variableName, string intervalTranslator, int min, int max)
             : base(LoM.INTERVAL, variableName)
        {
            this.intervalTranslator = intervalTranslator;
            this.distribution = distribution;

            this.distMin = distribution.Values.Min();
            this.distMax = distribution.Values.Max();
            this.zBoundDistMin = 0;
            this.zBoundDistMax = (distMax == 0) ? 0 : distMax;
            this.distRange = distMax - distMin;
            this.zBoundDistRange = zBoundDistMax - zBoundDistMin;

            this.min = min;
            this.max = max;
            this.range = max - min;
        }

        public float NormalizeToRange(int value)
        {
            return (((float)value-min) / range);
        }
    }

    /// <summary>
    /// Holds information about:
    ///     * level of measurement
    ///     * name
    ///     * minimum, maximum and range
    ///     * zero bound minimum, maximum and range
    /// </summary>
    public class RatioAttributeStats : AttributeStats
    {
        public float range, zeroBoundRange;
        public float min, zeroBoundMin;
        public float max, zeroBoundMax;
        public string variableUnit;

        public RatioAttributeStats(
            string variableName,
            float range, float zeroBoundRange,
            float min, float zeroBoundMin,
            float max, float zeroBoundMax) : base(LoM.RATIO, variableName)
        {
            this.range = range;
            this.zeroBoundRange = zeroBoundRange;
            this.min = min;
            this.zeroBoundMin = zeroBoundMin;
            this.max = max;
            this.zeroBoundMax = zeroBoundMax;
            this.name = variableName;
        }

        public float NormalizeToRange(float value)
        {
            return (value - min) / range;
        }

        public float NormalizeToZeroBoundRange(float value)
        {
            return (value - zeroBoundMin) / zeroBoundRange;
        }
    }


}
