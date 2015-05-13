using System;
using System.Collections;
using System.Collections.Generic;

using BookLibrary.Model;
using BookLibrary;

namespace BookLibrary.Collection{
	
	class BorrowedBookCollection{
		
		private Dictionary<long, BorrowedBook> borrowedBooks = new Dictionary<long, BorrowedBook> ();

		public List<BorrowedBook> getAll(){
			List<BorrowedBook> result = new List<BorrowedBook> ();
			foreach (var bbook in this.borrowedBooks) {
				result.Add (bbook.Value);
			}
			return result;
		}

		public BorrowedBook borrowBook(Member borrower, Book book, DateTime date){
			BorrowedBook borrowedBook = new BorrowedBook (){ book = book, borrower = borrower, time=date };
			this.borrowedBooks.Add (book.id, borrowedBook);
			return borrowedBook;
		}

		public bool isBookAvailable(long bookId){
			return !this.borrowedBooks.ContainsKey (bookId);
		}

		public List<Book> getOverdueBooks(){
			List<Book> result = new List<Book>();

			foreach (var borrowedBook in this.borrowedBooks) {
				if (new TimeSpan ((DateTime.UtcNow.Ticks - borrowedBook.Value.time.Ticks)).TotalSeconds > Constant.OVERDUE_TIME) {
					result.Add (borrowedBook.Value.book);
				}
			}

			return result;

		}

	}

	class BookCollection{
		private Dictionary<long, Book> books = new Dictionary<long, Book>();

		public List<Book> getAll(){
			List<Book> result = new List<Book> ();
			foreach (var book in this.books) {
				result.Add (book.Value);
			}
			return result;
		}

		public Book AddBook(string title, string author){
			Book book = new Book (){ title = title, author = author };

			this.books.Add (book.id, book);

			return book;
		}

		public Book getBookById(long id){
			return books [id];
		}

		public List<Book> searchByTitle(string title){
			List<Book> result = new List<Book> ();
			foreach (var book in this.books) {
				if (book.Value.title.Contains (title)) {
					result.Add (book.Value);
				}
			}
			return result;
		}

		public List<Book> searchByAuthor(string author){
			List<Book> result = new List<Book> ();
			foreach (var book in this.books) {
				if (book.Value.author.Contains (author)) {
					result.Add (book.Value);
				}
			}
			return result;
		}
	}

	class MemberCollection{
		private Dictionary<long, Member> members = new Dictionary<long, Member>();

		public List<Member> getAll(){
			List<Member> result = new List<Member> ();
			foreach (var member in this.members) {
				result.Add (member.Value);
			}
			return result;
		}

		public Member getMemberById(long id){
			return members [id];
		}

		public List<Member> searchByFname(string fname){
			List<Member> result = new List<Member> ();
			foreach (var member in this.members) {
				if (member.Value.fname.Contains (fname)) {
					result.Add (member.Value);
				}
			}
			return result;
		}

		public List<Member> searchByLname(string lname){
			List<Member> result = new List<Member> ();
			foreach (var member in this.members) {
				if (member.Value.lname.Contains (lname)) {
					result.Add (member.Value);
				}
			}
			return result;
		}

		public Member AddMember(string fname, string lname){
			Member member = new Member (){ fname = fname, lname = lname };
			members.Add (member.id, member);
			return member;
		}
	}
}
