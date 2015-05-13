using System;
using System.Collections;
using System.Collections.Generic;
//using BookLibrary;

namespace BookLibrary.Model{

	class Book{
		public long id { get { return this.GetHashCode ();} }
		public string title { get; set;}
		public string author { get; set;}

		public override string ToString ()
		{
			return string.Format ("[Book: id={0}, title={1}, author={2}]", id, title, author);
		}
	}

	class Member{
		public long id { get { return this.GetHashCode ();} }
		public string fname { get; set;}
		public string lname { get; set;}

		public override string ToString ()
		{
			return string.Format ("[Member: id={0}, fname={1}, lname={2}]", id, fname, lname);
		}
	}

	class BorrowedBook{		
		public Book book { get; set; }
		public DateTime time { get; set; }
		public Member borrower { get; set; }

		public override string ToString ()
		{
			return string.Format ("[BorrowedBook: book={0}, time={1}, borrower={2}]", book, time, borrower);
		}

	}

}