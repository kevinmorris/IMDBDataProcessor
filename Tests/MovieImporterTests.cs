using IMDBDataProcessor;

namespace Tests
{
    public class MovieImporterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MovieJsonImportParse()
        {
            const string json = @"[
                {
                    ""@type"": ""ListItem"",
                    ""position"": ""9920"",
                    ""url"": ""/title/tt1881109/""
                },
                {
                    ""@type"": ""ListItem"",
                    ""position"": ""9921"",
                    ""url"": ""/title/tt3297330/""
                }
            ]";

            var imdbIds = MovieImporter.Import(json);
            CollectionAssert.AreEqual(
                new List<string> { "tt1881109", "tt3297330" }, 
                imdbIds);
        }
    }
}