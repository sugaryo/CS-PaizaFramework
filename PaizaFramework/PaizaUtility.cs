using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaizaFramework
{
	/// <summary>
	/// paiza の出題回答に使用する汎用的なユーティリティ処理を纏める。
	/// </summary>
	public static class PaizaUtility
	{
		/// <summary>
		/// プロキシパターンでIOの向き先を変える為のインタフェース
		/// </summary>
		public interface ITestIO
		{
			string ReadLine();
			void WriteLine( string line );
		}
		/// <summary>
		/// ITestIO のデフォルト実装。
		/// </summary>
		public class ConsoleProxy : ITestIO
		{
			string ITestIO.ReadLine()
			{
				return Console.ReadLine();
			}

			void ITestIO.WriteLine( string line )
			{
				Console.WriteLine( line );
			}
		}

		public static ITestIO IO { get; set; } = new ConsoleProxy();
		
		
		public static string ReadLine()
		{
			return IO.ReadLine();
		}

		public static void WriteLine( string line )
		{
			IO.WriteLine( line );
		}

		/// <summary>
		/// 問題の入力データ取得（入力データ行数が未定の場合）
		/// </summary>
		/// <remarks>
		/// 入力データ数が予め定まっていない（入力データ数が可変）場合、
		/// 一行目にデータ数が入力され、その後、指定された数だけデータ行の入力が行われる。
		/// 一行目が数値に parse 出来なかった場合は、エラーとする。
		/// なお、一行目で入力された数と異なる行数が入力されるケースは出題の本旨と異なるため想定しない。
		/// </remarks>
		/// <returns>入力データの列挙</returns>
		public static IEnumerable<string> ReadArgs()
		{
			// 最初の入力が、後続するデータの行数になるらしい。
			string x = IO.ReadLine();
			int n = int.Parse(x);

			// 入力された行数ぶん、データ行の読み込みを行う。
			return ReadArgs( n );
		}
		/// <summary>
		/// 問題の入力データ取得（予め入力行数が解っているパターン）
		/// </summary>
		/// <param name="n">入力行数</param>
		/// <returns>入力データの列挙</returns>
		public static IEnumerable<string> ReadArgs(int n)
		{
			// 入力された行数ぶん、データ行の読み込みを行う。
			for ( int i = 0; i < n; i++ )
			{
				string s = IO.ReadLine();

				yield return s;
			}
		}
	}
}
