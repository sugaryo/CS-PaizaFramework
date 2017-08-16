using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PaizaFramework
{
	class Program
	{
		/// <summary>
		/// <seealso cref="Console.ReadLine"/> の替わりに、固定のテストデータを返す <seealso cref="PaizaUtility.ITestIO"/> 実装
		/// </summary>
		private class TestData : PaizaUtility.ITestIO
		{
			/// <summary>
			/// テストデータ
			/// </summary>
			private readonly string[] lines = @"

// ▼▼ここにテストデータをコピペするのじゃ▼▼
4
abcd
efgh
hgfe
dcba
5
abfgf
bfgc
abfga
hdc
fghde
// ▲▲ '//' 開始と空行は無視するから気にするな▲▲

"
				// ↓不要な LINQメソッド式 があれば適当にコメントアウトしてね↓
				.Split(new [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries )
				.AsEnumerable()
				.Select( s => s.Trim() )
				.Where( s => !string.IsNullOrEmpty(s) )
				.Where( s => !s.StartsWith("//") )
				.ToArray()
			;

			private int index = 0;
			
			string PaizaUtility.ITestIO.ReadLine()
			{
				return index < lines.Length ? lines[index++] : null;
			}

			void PaizaUtility.ITestIO.WriteLine( string line )
			{
				Console.WriteLine( line );
			}
		}

		/// <summary>
		/// デフォルトでConsoleに向いているIOを↑のデバッグ用プロキシ実装に差し替える。
		/// </summary>
		static Program()
		{
			// もうちょっとカッコイイ実装（JavaのCDIみたいな）にしたかったけど、paiza用なんでこれで良いよね。
			PaizaUtility.IO = new TestData();
		}
		
		/// <summary>
		/// ぶっちゃけ例外処理要らないみたいだから Do.Answer() だけ移植でも良い。
		/// </summary>
		static void Main( string[] args )
		{
			try
			{
				Do.Answer();
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
			}

#if DEBUG
			Console.ReadKey();
#endif
		}
	}
}
