using System;

namespace ArgParse{

	class ArgParser{

		public string command;
		public string[] parameters;
		private int next = -1;

		public override string ToString ()
		{						
			return string.Format ("[ArgParser]. command = {0}, params.len = {1}", this.command, this.parameters.Length);
		}

		public static ArgParser parse(string args){
			string[] strs = args.Split ();
			string[] param = new string[strs.Length - 1];

			Array.Copy (strs, 1, param, 0, strs.Length - 1);

			ArgParser argParser = new ArgParser (strs [0], param);

			return argParser;
		}


		ArgParser(string command, string[] parameters){
			this.command = command;
			this.parameters = parameters;
		}

		public string getNextParam(){
			next++;
			return this.parameters.Length > this.next ? this.parameters [next] : String.Empty;
		}

	}
}
