# Top100
A .NET form application to edit an excel file that will be used to have My Greatest Hits Songs of my life
Help from Chat GPT:
https://chatgpt.com/c/680a5868-cd48-8009-9374-901d999f682e

Excel file can be found at 
-	E:\Personal\Personal
-	D:\Personal
-	D:\BILL SIG\DEV\GitHub\BillSig\Top100
-	Dell Laptop desktop 
Dell Laptop is the up to date xls file 

Videos found in
-	C:\Users\Windows 11\Videos\Top100


Ways to download videos from Youtube:
https://chat.deepseek.com/a/chat/s/1af0bd03-81e9-4239-803b-a2e641e3d39c

The application will only have a main form with a Open File component in order to load a single excel file. 
Upon selecting the Excel file, the contents of its 1st sheet will be displayed in a grid. 
Row click will be enabled. 
The functionality of the grid will be the following: 

1) Ability to edit a row: 
	The user would double-click a row and a pop up form would appear with the values of each cell. 
	The user would be able to edit the values of the cell and click Update or Discard buttons to update the excel row or discard any changes respectively. 
2) Ability to reorder rows: 
	The user would select a row and then use Up and Down buttons in order to move the row up or down in the grid. 
	Update and Discard buttons would update the excel file or discard any changes.
	
Technical Specifications
-----------------------------
	
1. UI Framework and .NET Version
	WinForms
	Target framework: .NET 8

2. Excel Library
	Simple excel read/write
	Work only with plain cell values. We might add features in the future that could be translated to Excel formulas.

3. Header Row & Data Types
	The first row in the sheet contains column headers that will be displayed as grid column names
	All columns are text/string
	
4. Editing Workflow
	Updating a row would commit immediately all changes to file.
	Discard should revert the changes of the selected row.

5. Reordering Workflow
	Moving a row Up and Down stages the reorder-in-grid and only after clicking on grid's Update button will the changes become permanent.
	Discard should undo all changes.

6. Additional Features
	All CRUD functionality would be available.
	Filtering and ordering is a great nice-to-have feature
	Excel file should not exceed 500 rows.
	
TODO List:
-	Functionality
	o	Update Button: next to Load Excel button. Upon click, the file is saved as is. If no changes exist, it remains disabled.
	o	Up and Down Arrow buttons: replaces the selected record upwards or downwards respectively. Changes are not automatically saved.
	o	Discard Button: discards all changes after user’s confirmation.
	o	Edit Button (Double click a row): A modal form opens with the record data. User may edit all values except Position. OK/Cancel buttons on the modal ensures that the changes are kept in memory. They are not saved however, until user clicks on Update button.
	o	Show that changes are not saved: Update Button becomes enabled
	o	Load Button: show warning if there are any unsaved data.
-	Validation
	o	Excel file should not exceed 500 rows. A warning should be displayed in a modal window when loading the file and before saving it. 
	o	Saving changes will not be allowed if file exceeds 500 rows.
