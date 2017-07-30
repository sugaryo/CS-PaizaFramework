using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaizaFramework
{
	public static class Do
	{
		/// <summary>
		/// paiza の出題に回答するロジックを実装する
		/// </summary>
		public static void Answer()
		{
			// 【サンプル実装】ヘッダパーサを独自に指定するパターン。
			// ヘッダで２種類のデータ数が指定され、
			// それらが連続したデータ行として与えられる、みたいなケースを想定。
			// （実際にそんな問題があるのかどうかは知らん）
			int a = 0;
			int b = 0;
			Func<string, int> parser = ( header ) =>
			{
				int[] token = PaizaUtility
						.SplitAsInt( header )
						.ToArray();

				// 二種類のデータ数をそれぞれ控える。
				a = token[0];
				b = token[1];

				// 二種類のデータの合計数を返す（読み込み行数）
				return a + b;
			};
			// ヘッダのパーサを指定して ReadArgs を呼び出す。
			var args = PaizaUtility.ReadArgs( parser ).ToList();

			List<string> argsA = args
					.Take( a )
					.ToList();
			List<string> argsB = args
					.Skip( a )
					.Take( b )
					.ToList();
			
			foreach ( var arg in argsA )
			{
				PaizaUtility.IO.WriteLine( "arg-a: " + arg );
			}
			foreach ( var arg in argsB )
			{
				PaizaUtility.IO.WriteLine( "arg-b: " + arg );
			}
		}
	}
}
