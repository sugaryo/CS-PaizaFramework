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
			var args = PaizaUtility.ReadArgs().ToList();

			foreach ( var arg in args )
			{
				// 【サンプル実装】入力から受け取ったパラメータをそのまま復唱する。
				PaizaUtility.IO.WriteLine( "arg: " + arg );
			}
		}
	}
}
