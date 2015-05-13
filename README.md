# LibraryManagement
C# Testing with mono


it's a console application. available commands are described bellow.

Usage:
new [—member <fname> <lname> | —book <title> <author>]
search [—member [[—fname | —lname] <name>]  | —book [[—author | —title] <name> ] ]
borrow <member_id> <book_id>
list [—book [—overdue]  | —member  ] -l <limit>
exit

unit test fixture is added and can be found in folder 'test'. NUnit is used as the unit test framework.
details can be found here:http://www.nunit.org/.
