using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PaizaFramework
{
	class Program
	{
		static void Main( string[] args )
		{
			try
			{
				Answer();
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
			}
		}

		/// <summary>
		/// paiza の出題に回答するロジックを実装する
		/// </summary>
		/// <remarks>
		/// 実際の回答時には、インナークラスとして PaizaUtility をコピペして使用する。
		/// </remarks>
		private static void Answer()
		{
			var args = PaizaUtility.ReadArgs().ToList();

			foreach ( var arg in args )
			{
				Console.WriteLine( "arg: " + arg );
			}
		}
	}
}
