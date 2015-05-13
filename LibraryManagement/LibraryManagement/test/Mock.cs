using System;
using System.Collections.Generic;

namespace Mocking{ 
	class Mock{

		public static List<string> createStrings(int num, string seed){
			List<string> strs = new List<string> ();
			for (int i = 1; i <= num; i++) {
				strs.Add (seed + i);
			}
			return strs;
		}

//		public static List<Book> createBooks(int num){
//			List<Book> books = new List<Book> ();
//			for (int i = 0; i < num; i++) {
//				books.Add (new Book (){ title = "title" + num, author = "author" + num });
//			}
//			return books;
//		}
//
//
//		public static List<Member> createMembers(int num){
//			List<Member> members = new List<Member> ();
//			for (int i = 0; i < num; i++) {
//				members.Add (new Member (){ fname = "fname" + num, lname = "lname" + num });
//			}
//			return members;
//		}
	}
}
