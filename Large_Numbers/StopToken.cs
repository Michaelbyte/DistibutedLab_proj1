using System;
namespace Large_Numbers
{
	public class StopToken
	{
		public bool IsStopRequested { get; private set; } = false;

		public void RequestStop()
		{
			IsStopRequested = true;
		}
	}
}