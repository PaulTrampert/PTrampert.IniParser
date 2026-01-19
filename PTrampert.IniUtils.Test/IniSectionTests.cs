namespace PTrampert.IniUtils.Test
{
    [TestFixture]
    public class IniSectionTests
    {
        [Test]
        public void Include_MergesNonOverlappingKeys()
        {
            // Arrange
            var target = new IniSection { Name = "section" };
            target.KeyValues.Add("k1", new[] { "v1" });

            var other = new IniSection { Name = "section" };
            other.KeyValues.Add("k2", new[] { "v2" });

            // Act
            target.Include(other);

            // Assert
            Assert.That(target.KeyValues.ContainsKey("k1"), Is.True);
            Assert.That(target.KeyValues.ContainsKey("k2"), Is.True);
            Assert.That(target.KeyValues["k2"].First(), Is.EqualTo("v2"));
        }

        [Test]
        public void Include_MergesOverlappingKeys_UnionBehavior()
        {
            // Arrange
            var target = new IniSection { Name = "section" };
            target.KeyValues.Add("k", new[] { "a", "b" });

            var other = new IniSection { Name = "section" };
            other.KeyValues.Add("k", new[] { "b", "c" });

            // Act
            target.Include(other);

            // Assert
            var merged = target.KeyValues["k"].ToArray();
            // Implementation currently uses Enumerable.Union, so duplicates are removed
            Assert.That(merged, Is.EqualTo(new[] { "a", "b", "c" }));
        }

        [Test]
        public void Include_DifferentSectionName_ThrowsArgumentException()
        {
            // Arrange
            var target = new IniSection { Name = "one" };
            var other = new IniSection { Name = "two" };
            other.KeyValues.Add("k", new[] { "v" });

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => target.Include(other));
            Assert.That(ex.ParamName, Is.EqualTo("other"));
        }
    }
}
