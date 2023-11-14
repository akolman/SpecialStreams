using AppendableStream;
using SpecialStreams;

namespace SpecialStreams.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void TestSubStream()
		{
			var data = "Hello There.  Start of sub stream.  End of sub stream";
			var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data));
			var subStream = new SubStream(stream, 14, 34);

			var reader = new StreamReader(subStream);

			var line = reader.ReadLine();

			Assert.Equal("Start of sub stream.", line);

			subStream.Seek(0, SeekOrigin.Begin);

			Assert.Equal(0, subStream.Position);

			Assert.Equal(20, subStream.Length);
		}

		[Fact]
		public void TestCollectiveStream()
		{
			var data1 = "Hello There.  Start of sub stream.  End of sub stream";
			var stream1 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data1));
			var subStream1 = new SubStream(stream1, 14, 34);

			var data2 = "Hello There.  Start of sub stream2.  End of sub stream";
			var stream2 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data2));
			var subStream2 = new SubStream(stream2, 14, 35);

			var data3 = "Hello There.  Start of sub stream3a.  End of sub stream";
			var stream3 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data3));
			var subStream3 = new SubStream(stream3, 14, 36);

			var subStreams = new List<SubStream>() { subStream1, subStream2, subStream3 };

			var collectiveStream = new CollectiveStream(subStreams);

			var streamReader = new StreamReader(collectiveStream);

			var data = streamReader.ReadLine();
		}

		[Fact]
		public void TestCollectiveStreamNotSubstream()
		{
			var data1 = "Hello There.  Start of sub stream.  End of sub stream";
			var stream1 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data1));

			var data2 = "Hello There.  Start of sub stream2.  End of sub stream";
			var stream2 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data2));

			var data3 = "Hello There.  Start of sub stream3a.  End of sub stream";
			var stream3 = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data3));

			var subStreams = new List<Stream>() { stream1, stream2, stream3 };

			var collectiveStream = new CollectiveStream(subStreams);

			var streamReader = new StreamReader(collectiveStream);

			var data = streamReader.ReadLine();
		}
	}
}