using System.Linq;
using NUnit.Framework;
using PTrampert.IniUtils;

namespace PTrampert.IniUtils.Test
{
    [TestFixture]
    public class IniFileTests
    {
        [Test]
        public void Include_MergesNonOverlappingSections()
        {
            // Arrange
            var target = new IniFile();
            var aSection = new IniSection { Name = "A" };
            aSection.KeyValues.Add("k1", new[] { "v1" });
            target.Sections.Add("A", aSection);

            var other = new IniFile();
            var bSection = new IniSection { Name = "B" };
            bSection.KeyValues.Add("k2", new[] { "v2" });
            other.Sections.Add("B", bSection);

            // Act
            target.Include(other);

            // Assert
            Assert.That(target.Sections.ContainsKey("A"), Is.True);
            Assert.That(target.Sections.ContainsKey("B"), Is.True);
            Assert.That(target.Sections["B"].KeyValues["k2"].First(), Is.EqualTo("v2"));
        }

        [Test]
        public void Include_MergesOverlappingSections_MergesKeys()
        {
            // Arrange
            var target = new IniFile();
            var s1 = new IniSection { Name = "Common" };
            s1.KeyValues.Add("k", new[] { "a" });
            target.Sections.Add("Common", s1);

            var other = new IniFile();
            var s2 = new IniSection { Name = "Common" };
            s2.KeyValues.Add("k", new[] { "b" });
            other.Sections.Add("Common", s2);

            // Act
            target.Include(other);

            // Assert
            var merged = target.Sections["Common"].KeyValues["k"].ToArray();
            Assert.That(merged, Is.EqualTo(new[] { "a", "b" }));
        }

        [Test]
        public void Include_AddsSectionByReference_WhenSectionDidNotExist()
        {
            // Arrange
            var target = new IniFile();
            var other = new IniFile();
            var newSection = new IniSection { Name = "New" };
            newSection.KeyValues.Add("x", new[] { "1" });
            other.Sections.Add("New", newSection);

            // Act
            target.Include(other);

            // Assert: the implementation adds the other section instance directly
            Assert.That(object.ReferenceEquals(target.Sections["New"], newSection), Is.True);
        }
    }
}
