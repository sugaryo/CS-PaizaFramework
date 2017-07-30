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

		/// <summary>
		/// 問題の入力データ取得（複雑なヘッダ形式をしている場合）
		/// </summary>
		/// <param name="parseHeaderRecord">ヘッダレコードを解析し、その後読み込むべきデータ行数を返すコールバックメソッド</param>
		/// <returns>入力データの列挙</returns>
		public static IEnumerable<string> ReadArgs( Func<string, int> parseHeaderRecord )
		{
			string header = IO.ReadLine();
			int n = parseHeaderRecord( header );

			return ReadArgs( n );
		}


		#region 普段なら拡張メソッドで追加するんだけどpaiza回答コードだと微妙だったので。
		/// <summary>
		/// 指定した文字列を指定した分割子で分割します。
		/// </summary>
		/// <remarks>
		/// このメソッドは以下の実装とほぼ等価です。
		/// <code>
		/// return s.Split( new []{ by }, StringSplitOptions.None );
		/// // by パラメータは params なので、実際には new[] は不要。
		/// </code>
		/// <param name="s">分割する文字列</param>
		/// <param name="by">分割子（params parameter）</param>
		/// <returns>分割子で分割した文字列配列</returns>
		public static string[] SplitBy(
				string s, 
				params string[] by )
		{
			return SplitBy( s, StringSplitOptions.None, by );
		}
		/// <summary>
		/// 指定した文字列を指定した分割子で分割します。
		/// </summary>
		/// <remarks>
		/// このメソッドは以下の実装とほぼ等価です。
		/// <code>
		/// return s.Split( new []{ by }, option );
		/// // by パラメータは params なので、実際には new[] は不要。
		/// </code>
		/// </remarks>
		/// <param name="s">分割する文字列</param>
		/// <param name="option">分割オプション（<seealso cref="StringSplitOptions"/>）</param>
		/// <param name="by">分割子（params parameter）</param>
		/// <returns>分割子で分割した文字列配列</returns>
		public static string[] SplitBy( 
				string s, 
				StringSplitOptions option,
				params string[] splitter )
		{
			return s.Split( splitter, option );
		}

		/// <summary>
		/// 指定した文字列を半角スペース <c>" "</c> で分割します。
		/// </summary>
		/// <remarks>
		/// paiza の出題で最も頻出する、スペースで区切られたトークンを処理する為のユーティリティです。
		/// </remarks>
		/// <param name="s">分割する文字列</param>
		/// <returns>分割した文字列配列</returns>
		public static string[] SplitSpace( string s )
		{
			return SplitBy( s, " " );
		}
		/// <summary>
		/// 指定した文字列を半角スペース <c>" "</c> で分割し、パーサを通して任意の型に変換して返します。
		/// </summary>
		/// <typeparam name="T">返す型</typeparam>
		/// <param name="s">分割する文字列</param>
		/// <param name="parser">パーサ（分割した文字列トークンを任意の型に変換するデリゲートを指定して下さい）</param>
		/// <returns>分割、パースした <code>T</code>型の列挙</returns>
		public static IEnumerable<T> SplitAs<T>( string s, Func<string, T> parser )
		{
			return SplitSpace( s ).Select( parser );
		}
		/// <summary>
		/// 指定した文字列を半角スペース <c>" "</c> で分割し、<seealso cref="int"/> で返します。
		/// </summary>
		/// <remarks>
		/// paiza の出題で最も頻出する、スペースで区切られた整数トークンを処理する為のユーティリティです。
		/// </remarks>
		/// <param name="s">分割する文字列</param>
		/// <returns>分割、パースした<seealso cref="int"/>の列挙</returns>
		public static IEnumerable<int> SplitAsInt( string s )
		{
			return SplitAs( s, int.Parse );
		}
		#endregion
	}
}
