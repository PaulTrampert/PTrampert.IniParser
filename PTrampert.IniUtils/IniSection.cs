using System;
using System.Collections.Generic;
using System.Linq;

namespace PTrampert.IniUtils;

/// <summary>
/// Data structure representing a section in an INI file. Name is the section name,
/// and KeyValues contains the key-value pairs within that section. Values are stored
/// in the order that they are read from the file.
/// </summary>
public class IniSection
{
    /// <summary>
    /// The section name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Dictionary of key-value pairs in this section. Each key maps to an enumerable of values,
    /// preserving the order in which they were read from the INI file.
    /// </summary>
    public Dictionary<string, IEnumerable<string>> KeyValues { get; } = new();

    /// <summary>
    /// Merge another IniSection into this one. Keys from the other section
    /// will be added to this section. If a key already exists, the values
    /// from the other section will be merged in.
    /// </summary>
    /// <param name="other">The other section to merge into this one.</param>
    public void Include(IniSection other)
    {
        if (this.Name != other.Name)
        {
            throw new ArgumentException("Cannot include section with different name", nameof(other));
        }

        foreach (var key in other.KeyValues.Keys)
        {
            if (this.KeyValues.TryGetValue(key, out var values))
            {
                KeyValues[key] = values.Union(other.KeyValues[key]);
            }
            else
            {
                KeyValues.Add(key, other.KeyValues[key]);
            }
        }
    }
}