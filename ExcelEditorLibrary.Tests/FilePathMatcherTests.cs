using ExcelEditorLibrary.Abstractions;
using ExcelEditorLibrary.Services;

namespace ExcelEditorLibrary.Tests
{
    public class FilePathMatcherTests : IDisposable
    {
        private readonly string testFolder;

        public FilePathMatcherTests()
        {
            testFolder = Path.Combine(Path.GetTempPath(), "FilePathMatcherTests_" + Guid.NewGuid());
            Directory.CreateDirectory(testFolder);
        }

        public void Dispose()
        {
            if (Directory.Exists(testFolder))
            {
                Directory.Delete(testFolder, recursive: true);
            }
        }

        private class FakeSearchableItem : IHasSearchableFilePath
        {
            public string[] Tokens { get; set; }
            public string FilePath { get; set; }

            public IEnumerable<string> GetSearchTokens() => Tokens;
        }

        /// <summary>
        /// Test-only IProgress&lt;T&gt; implementation that invokes the callback
        /// synchronously and in-order, unlike System.Progress&lt;T&gt; which posts
        /// callbacks to a SynchronizationContext (or the thread pool) asynchronously.
        /// </summary>
        private class SynchronousProgress<T> : IProgress<T>
        {
            private readonly Action<T> callback;

            public SynchronousProgress(Action<T> callback)
            {
                this.callback = callback;
            }

            public void Report(T value) => callback(value);
        }

        [Fact]
        public void UpdateFilePaths_SetsFilePath_WhenAllTokensFoundInFileName()
        {
            // Arrange
            var filePath = Path.Combine(testFolder, "Queen - Bohemian Rhapsody.mp4");
            File.WriteAllText(filePath, string.Empty);

            var item = new FakeSearchableItem { Tokens = new[] { "Queen", "Bohemian Rhapsody" } };
            var matcher = new FilePathMatcher();

            // Act
            var result = matcher.UpdateFilePaths(new[] { item }, testFolder);

            // Assert
            Assert.Equal(filePath, item.FilePath);
            Assert.Equal(1, result.MatchedCount);
            Assert.Equal(0, result.UnmatchedCount);
        }

        [Fact]
        public void UpdateFilePaths_LeavesFilePathNull_WhenNotAllTokensFoundInAnyFileName()
        {
            // Arrange
            File.WriteAllText(Path.Combine(testFolder, "Queen - Bohemian Rhapsody.mp4"), string.Empty);

            var item = new FakeSearchableItem { Tokens = new[] { "Queen", "Another One Bites The Dust" } };
            var matcher = new FilePathMatcher();

            // Act
            var result = matcher.UpdateFilePaths(new[] { item }, testFolder);

            // Assert
            Assert.Null(item.FilePath);
            Assert.Equal(0, result.MatchedCount);
            Assert.Equal(1, result.UnmatchedCount);
            Assert.Contains("Queen - Another One Bites The Dust", result.UnmatchedDescriptions);
        }

        [Fact]
        public void UpdateFilePaths_ReportsProgress_OncePerItemInOrder()
        {
            // Arrange
            File.WriteAllText(Path.Combine(testFolder, "Queen - Bohemian Rhapsody.mp4"), string.Empty);
            File.WriteAllText(Path.Combine(testFolder, "ACDC - Thunderstruck.mp4"), string.Empty);

            var items = new[]
            {
                new FakeSearchableItem { Tokens = new[] { "Queen", "Bohemian Rhapsody" } },
                new FakeSearchableItem { Tokens = new[] { "ACDC", "Thunderstruck" } },
                new FakeSearchableItem { Tokens = new[] { "Unknown", "Song" } }
            };

            var reportedValues = new List<int>();
            var progress = new SynchronousProgress<int>(value => reportedValues.Add(value));
            var matcher = new FilePathMatcher();

            // Act
            var result = matcher.UpdateFilePaths(items, testFolder, progress);

            // Allow Progress<T> callbacks (posted to the captured SynchronizationContext) to complete.
            // In unit test context without a UI message loop, Progress<T> invokes synchronously,
            // so no extra wait is needed here, but we assert the final expected state regardless.

            // Assert
            Assert.Equal(2, result.MatchedCount);
            Assert.Equal(1, result.UnmatchedCount);
            Assert.Equal(new[] { 1, 2, 3 }, reportedValues);
        }
    }
}