using System;
using System.Collections.Generic;
using NUnit.Framework;

using BookLibrary;
using BookLibrary.Model;

using Mocking;

namespace UnitTest
{
	class Helper{
		public static List<Member> createMembers(string fnameSeed, string lNameSeed, int num, LibraryManager libObj){
			List<string> fnames = Mock.createStrings (num, fnameSeed);
			List<string> lnames = Mock.createStrings (num, lNameSeed);

			List<Member> result = new List<Member> ();

			for (int i = 0; i < num; i++) {
				result.Add(libObj.newMember (fnames [i], lnames [0]));
			}

			return result;
		}

		public static List<Book> createBooks(string titleSeed, string authorSeed, int num, LibraryManager libObj){
			List<string> titles = Mock.createStrings (num, titleSeed);
			List<string> authors = Mock.createStrings (num, authorSeed);

			List<Book> result = new List<Book> ();

			for (int i = 0; i < num; i++) {
				result.Add(libObj.newBook (titles [i], authors [0]));
			}

			return result;
		}
	}

	[TestFixture]
	public class Test
	{		
		[Test]
		public void BookCreation(){
			LibraryManager lib = new LibraryManager ();
			Book b1 = lib.newBook ("t1", "a1");

			Assert.IsTrue (lib.getBookById (b1.id).id == b1.id);
			Assert.IsTrue (lib.getBookById (b1.id).title == b1.title);
			Assert.IsTrue (lib.getBookById (b1.id).author == b1.author);
		}

		[Test]
		public void MemberCreation(){
			LibraryManager lib = new LibraryManager ();
			Member m1 = lib.newMember ("f1", "l1");

			Assert.IsTrue (lib.getMemberById (m1.id).id == m1.id);
			Assert.IsTrue (lib.getMemberById (m1.id).fname == m1.fname);
			Assert.IsTrue (lib.getMemberById (m1.id).lname == m1.lname);

		}

		[Test]
		public void SearchMembersByFName(){
			LibraryManager lib = new LibraryManager ();

			Helper.createMembers ("steve", "brandon", 5, lib);
			Helper.createMembers ("robert", "langdon", 2, lib);
			Helper.createMembers ("ashiqur", "rahman", 1, lib);

			Assert.AreEqual (5, lib.searchMembersByFname ("steve").Count);
			Assert.AreEqual (2, lib.searchMembersByFname ("robert").Count);
			Assert.AreEqual (1, lib.searchMembersByFname ("ashiqur").Count);
			Assert.AreEqual (0, lib.searchMembersByFname ("miss").Count);
		}

		[Test]
		public void SearchBooksByTitle(){
			LibraryManager lib = new LibraryManager ();

			Helper.createBooks ("around the world in 80 days", "jule verne", 1, lib);
			Helper.createBooks ("book", "author", 5, lib);


			Assert.AreEqual (1, lib.searchBooksByTitle ("around the").Count);
			Assert.AreEqual (5, lib.searchBooksByTitle ("book").Count);
			Assert.AreEqual (1, lib.searchBooksByTitle ("book2").Count);
			Assert.AreEqual (0, lib.searchBooksByTitle ("miss").Count);
		}

		[Test]
		public void Borrow(){
			LibraryManager lib = new LibraryManager ();

			List<Member> ms = Helper.createMembers ("steve", "brandon", 3, lib);

			List<Book> bs = new List<Book> ();

			bs.AddRange ( Helper.createBooks ("the vinci code", "robert langdon", 1, lib));
			bs.AddRange(Helper.createBooks ("around the world in 80 days", "jule verne", 1, lib));

			BorrowedBook borrowedBook = lib.borrowBook (ms[2].id, bs[0].id);

			Assert.IsTrue (borrowedBook.book.id == bs [0].id);
			Assert.IsTrue (borrowedBook.borrower.id == ms [2].id);

		}

		[Test]
		public void GetAllMembers(){
			LibraryManager lib = new LibraryManager ();

			List<Member> ms = Helper.createMembers ("steve", "brandon", 6, lib);
			ms.AddRange (Helper.createMembers("ashiqur", "rahman", 4, lib));

			Assert.IsTrue (ms.Count == lib.getAllMembers ().Count);

		}

		[Test]
		public void GetOverDueBooks(){
			LibraryManager lib = new LibraryManager ();

			List<Member> ms = Helper.createMembers ("f", "l", 3, lib);
			List<Book> bs = Helper.createBooks ("t", "a", 10, lib);


			DateTime t = DateTime.UtcNow;
			TimeSpan span = new TimeSpan (8, 0, 0, 0);
			t = t.Subtract (span);

			Console.WriteLine (DateTime.UtcNow.Day);
			Console.WriteLine (t.Day);

			lib.borrowBook (ms [0].id, bs [0].id, t);

			lib.borrowBook (ms [0].id, bs [1].id, t);
			lib.borrowBook (ms [0].id, bs [2].id);
			lib.borrowBook (ms [0].id, bs [3].id);
			lib.borrowBook (ms [1].id, bs [4].id);

			List<Book> bbs = lib.getOverdueBooks ();
			Assert.AreEqual (2, bbs.Count);

		}
	}
}

