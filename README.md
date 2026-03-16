# Top100
A .NET form application to edit an excel file that will be used to have My Greatest Hits Songs of my life
Help from Chat GPT:
https://chatgpt.com/c/680a5868-cd48-8009-9374-901d999f682e

Excel file can be found at 
-	E:\Personal\Personal
-	D:\Personal
-	Dell Laptop desktop 
Dell Laptop is the up to date xls file 

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