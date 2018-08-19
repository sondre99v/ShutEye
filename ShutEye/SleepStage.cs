using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutEye
{
	public struct SleepStage
	{
		static public SleepStage None => new SleepStage(Stage.None);
		static public SleepStage Wake => new SleepStage(Stage.Wake);
		static public SleepStage REM => new SleepStage(Stage.REM);
		static public SleepStage Stage1 => new SleepStage(Stage.Stage1);
		static public SleepStage Stage2 => new SleepStage(Stage.Stage2);
		static public SleepStage Stage3 => new SleepStage(Stage.Stage3);

		static public bool TryParse(string s, out SleepStage result)
		{
			switch(s)
			{
				case "Wake":
					result = Wake;
					return true;
				case "REM":
					result = REM;
					return true;
				case "Stage 1":
					result = Stage1;
					return true;
				case "Stage 2":
					result = Stage2;
					return true;
				case "Stage 3":
					result = Stage3;
					return true;
				default:
					result = None;
					return false;
			}
		}

		static public SleepStage Parse(string s)
		{
			if(s == null) throw new ArgumentNullException();

			if(TryParse(s, out SleepStage result))
			{
				return result;
			}
			else
			{
				throw new FormatException($"\"{s}\" is not a valid SleepStage string.");
			}
		}

		public int GetValue()
		{
			return (int)_stage;
		}

		private enum Stage : byte
		{
			None = 0,
			Wake = 1,
			REM = 2,
			Stage1 = 3,
			Stage2 = 4,
			Stage3 = 5
		}

		private Stage _stage;

		private SleepStage(Stage stage)
		{
			_stage = stage;
		}
	}
}
