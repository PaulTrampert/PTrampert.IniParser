using System.Collections.Generic;

namespace PTrampert.IniUtils;

/// <summary>
/// Data structure representing an INI file.
/// </summary>
public class IniFile
{
    /// <summary>
    /// The sections in the INI file, keyed by section name. The top-level (global) section
    /// is represented by an empty string as the key.
    /// </summary>
    public Dictionary<string, IniSection> Sections { get; } = new();

    /// <summary>
    /// Merge another IniFile into this one. Sections and keys from the other file
    /// will be added to this file. If a section or key already exists, the values
    /// from the other file will be merged in.
    /// </summary>
    /// <param name="other">The other file to merge into this one.</param>
    public void Include(IniFile other)
    {
        foreach (var key in other.Sections.Keys)
        {
            if (this.Sections.TryGetValue(key, out var section))
            {
                section.Include(other.Sections[key]);
            }
            else
            {
                Sections.Add(key, other.Sections[key]);
            }
        }
    }
}