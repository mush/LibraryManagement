using System;
using System.Collections.Generic;
using System.Collections;

using BookLibrary;
using BookLibrary.Model;
using ArgParse;

namespace Main
{
	class Helper{
		public static void printList<T>(List<T> list){
			foreach(var item in list){
				Console.WriteLine (item.ToString ());
			}
		}
	}

	class COMMANDS{
		public const string EXIT = "exit";
		public const string NEW = "new";
		public const string SEARCH = "search";
		public const string BORROW = "borrow";
		public const string LIST = "list";

		public static string GetHelpForNew(){
			return "new [—member <fname> <lname> | —book <title> <author>]";
		}
		public static string GetHelpForSearch(){
			return "search [—member [[—fname | —lname] <name>]  | —book [[—author | —title] <name> ] ]";
		}
		public static string GetHelpForBorrow(){
			return "borrow <member_id> <book_id>";
		}
		public static string GetHelpForList(){
			return "list [—book [—overdue]  | —member [—borrower]  ] -l <limit> —all";
		}

		public static string GetHelpString(){
			return "usage:"+ GetHelpForNew() + Environment.NewLine +
				GetHelpForSearch() + Environment.NewLine +
				GetHelpForBorrow() + Environment.NewLine +
				GetHelpForList();
		}

	}

	class OPTIONS{		
		public const string MEMBER = "--member";
		public const string BOOK = "--book";

		public const string BY_AUTHOR = "--author";
		public const string BY_TITLE = "--title";

		public const string BY_FNAME = "--fname";
		public const string BY_LNAME = "--lname";


		public const string OVERDUE = "--overdue";

		public const string LIMIT = "-l";
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			string arguments;
			LibraryManager manager = new LibraryManager ();

			Console.WriteLine (COMMANDS.GetHelpString ());

			while ((arguments = Console.ReadLine ()) != COMMANDS.EXIT) {

				if (arguments.Length == 0)
					continue;

				ArgParser argp = ArgParser.parse (arguments);

				switch (argp.command) {
				case COMMANDS.NEW:
					string subcommand = "";
					try {
						subcommand = argp.parameters [0];
					} catch (IndexOutOfRangeException) {
						Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForNew());
						break;
					}

					if (subcommand == OPTIONS.MEMBER) {
						Member member;
						try {
							string fname = argp.parameters [1];
							string lname = argp.parameters [2];

							member = manager.newMember (fname, lname);
							Console.WriteLine ("new member added...");
							Console.WriteLine (member.ToString ());
						} catch (IndexOutOfRangeException) {
							Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForNew());
							break;
						}
					} else if (subcommand == OPTIONS.BOOK) {
						Book book;
						try {
							string title = argp.parameters [1];
							string author = argp.parameters [2];

							book = manager.newBook (title, author);
							Console.WriteLine ("new book added...");
							Console.WriteLine (book);

						} catch (IndexOutOfRangeException) {
							Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForNew());
							break;
						}
					} else {
						Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForNew());
					}

					break;
				case COMMANDS.BORROW:
					long memberId = -1;
					long bookId = -1;
					try{
						memberId = long.Parse (argp.parameters [0]);
						bookId = long.Parse (argp.parameters [1]);
					}catch{
						Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForBorrow());
						break;
					}

					try{
						BorrowedBook borrowedBook = manager.borrowBook (memberId, bookId);

						Console.WriteLine(string.Format("member '{0}' borrowed book '{1}'", borrowedBook.borrower.fname, borrowedBook.book.title));
					}catch(Exception){
						Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForBorrow());
						break;
					}
					break;
				case COMMANDS.LIST:
					//list [—book [—overdue]  | —member  ] -l <limit>

					string option = argp.getNextParam ();

					if (string.IsNullOrEmpty (option)) {
						Console.WriteLine ("invalid command:" + COMMANDS.GetHelpForList ());
						break;
					}

					if (option == OPTIONS.BOOK) {
						string opt2 = argp.getNextParam ();
						//--overdue
						if (opt2 == OPTIONS.OVERDUE) {
							List<Book> bs = new List<Book> ();
							bs = manager.getOverdueBooks ();
							Console.WriteLine ("following are the overdue books");
							Helper.printList (bs);
							break;
						} else {
							List<Book> bs = new List<Book> ();
							bs = manager.getAllBooks ();

							if (opt2 == OPTIONS.LIMIT) { //-l <limit>
								int limit = int.Parse (argp.getNextParam ());
								bs.RemoveRange (0, Math.Max(0, bs.Count - limit));
							}

							Console.WriteLine ("following are the books");
							Helper.printList (bs);
						}

					} else if (option == OPTIONS.MEMBER) {//--member
						List<Member> ms = manager.getAllMembers();
						if(argp.getNextParam() == OPTIONS.LIMIT){
							int limit = int.Parse(argp.getNextParam());
							ms.RemoveRange(0, Math.Max(0, ms.Count - limit));
						}
						Console.WriteLine ("following are the members");
						Helper.printList (ms);
					}

					break;
				case COMMANDS.SEARCH:
					//search [—member [[—fname | —lname] <name>]  | —book [[—author | —title] <name> ] ]

					option = argp.getNextParam ();
					if (option == OPTIONS.BOOK) {
						//[[—author | —title] <name> ] ]
						string opt2 = argp.getNextParam ();
						List<Book> bs = new List<Book> ();
						if (opt2 == OPTIONS.BY_AUTHOR) {
							string author = argp.getNextParam ();
							bs = manager.searchBooksByAuthor (author);
						} else if (opt2 == OPTIONS.BY_TITLE) {
							string title = argp.getNextParam ();
							bs = manager.searchBooksByTitle (title);
						}
						Console.WriteLine ("following are the books");
						Helper.printList (bs);
					} else if (option == OPTIONS.MEMBER) {
						//[[—fname | —lname] <name>]
						string opt2 = argp.getNextParam ();
						List<Member> ms = new List<Member> ();
						if (opt2 == OPTIONS.BY_FNAME) {
							string fname = argp.getNextParam ();
							ms = manager.searchMembersByFname (fname);
						}else if (opt2 == OPTIONS.BY_LNAME){
							string lname = argp.getNextParam ();
							ms = manager.searchMembersByLname (lname);
						}
						Console.WriteLine ("following are the books");
						Helper.printList (ms);
					}

					break;
				default:
					Console.WriteLine ("invalid command");
					break;
				}


			}
		}
	}
}
