using System;
using System.Collections;
using System.Collections.Generic;

using BookLibrary.Model;
using BookLibrary.Collection;

namespace BookLibrary{

	static class Constant{
		public static long OVERDUE_TIME = 7 * 24 * 3600;
	}

	class LibraryManager{

		private BookCollection bookCollection = new BookCollection ();
		private MemberCollection memberCollection = new MemberCollection ();
		private BorrowedBookCollection borrowedBooks = new BorrowedBookCollection ();

		public Book newBook(string title, string author){
			return this.bookCollection.AddBook (title, author);
		}

		public Member newMember(string fname, string lname){
			return this.memberCollection.AddMember (fname, lname);
		}

		public Book getBookById(long id){
			return this.bookCollection.getBookById(id);
		}

		public Member getMemberById(long id){
			return this.memberCollection.getMemberById(id);
		}

		public List<Member> searchMembersByFname(string fname){
			return this.memberCollection.searchByFname (fname);
		}

		public List<Member> searchMembersByLname(string lname){
			return this.memberCollection.searchByLname (lname);
		}

		public List<Book> searchBooksByTitle(string title){
			return this.bookCollection.searchByTitle (title);
		}

		public List<Book> searchBooksByAuthor(string author){
			return this.bookCollection.searchByAuthor (author);
		}

		public BorrowedBook borrowBook(long memberId, long bookId){
			return this.borrowBook (memberId, bookId, DateTime.UtcNow);
		}

		public BorrowedBook borrowBook(long memberId, long bookId, DateTime date){
			Member borrower;
			Book book;
			try{
				borrower = this.memberCollection.getMemberById(memberId);
			}catch(KeyNotFoundException){
				throw new KeyNotFoundException ("no such member with id " + memberId); 
			}

			try{
				book = this.bookCollection.getBookById(bookId);
			}catch(KeyNotFoundException){
				throw new KeyNotFoundException ("no such book with id " + bookId); 
			}

			if (this.borrowedBooks.isBookAvailable (book.id)) {
				return borrowedBooks.borrowBook (borrower, book, date);
			} else {
				throw new Exception ("Book " + book.title+" is not available");
			}


		}

		public List<Book> getOverdueBooks(){
			return this.borrowedBooks.getOverdueBooks ();
		}

		public List<Member> getAllMembers(){
			return this.memberCollection.getAll ();
		}

		public List<Book> getAllBooks(){
			return this.bookCollection.getAll ();
		}
	}
}
