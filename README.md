# Excel Editor - Top 100 Greatest Hits Manager

A WinForms application for managing and editing an Excel file containing your personal "Greatest Hits" song collection. This tool provides an intuitive interface for CRUD operations, row reordering, and filtering capabilities.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

## 📋 Overview

Excel Editor is a desktop application designed to manage a curated list of favorite songs stored in an Excel file. It provides a user-friendly grid interface with full CRUD functionality, row reordering, and advanced filtering options.

## ✨ Features

### Current Version (v1.1)
- ✅ **Full CRUD Operations**: Create, Read, Update, and Delete records
- ✅ **Row Reordering**: Move songs up/down with visual feedback
- ✅ **Advanced Filtering**: Filter by band name, song title, and viewed status
- ✅ **Instant or Manual Save**: Configurable save behavior via `appsettings.json`
- ✅ **Unsaved Changes Tracking**: Visual indicators (yellow highlight) for modified rows
- ✅ **Data Validation**: 500-row limit enforcement with user warnings
- ✅ **Excel Format Support**: Compatible with `.xls` and `.xlsx` files
- ✅ **Logging**: Integrated Serilog for debugging and error tracking
- ✅ **Splash Screen**: Professional startup experience

### Data Model
Each record contains:
- **Position** (Integer): Row number/ranking
- **Band Name** (String): Artist or band name
- **Song Title** (String): Song name
- **Video Link** (String): URL to video (e.g., YouTube)
- **Viewed Data** (Boolean): Generic Flag indicating if the video has been watched / downloaded / archived or listed for further action

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 Runtime or SDK
- Windows OS (WinForms application)
- Excel file with the following structure:
  - First row: Header with column names
  - Columns: Position | Band | Song | Video Link | Viewed Data
  - Maximum 500 rows (excluding header)

### Installation and Deployment

1. **Clone the repository**

2. **Build the solution**

3. **Run the application**

### Configuration

Edit `appsettings.json` to customize application behavior:

**Configuration Options:**
- `MaxGreatestHitsCounter`: Maximum allowed rows (default: 500)
- `ColumnsCounter`: Expected number of columns (default: 5)
- `SaveToExcelInstantly`: 
  - `true`: Auto-save after each operation
  - `false`: Manual save required (Update button)

## 📖 Usage

### Loading an Excel File
1. Click **Load Excel** button
2. Select your `.xlsx` or `.xls` file
3. The grid will display all records from the first sheet

### Editing a Record
1. **Double-click** any row to open the edit dialog
2. Modify fields (Position is read-only)
3. Click **OK** to stage changes or **Cancel** to discard
4. Modified rows appear with **yellow background**
5. Click **Update** to save or **Discard** to revert

### Adding a New Record
1. Select a row (new record will insert at this position)
2. Click **Add** button
3. Fill in the details in the dialog
4. Click **OK** to insert the record
5. All subsequent rows will be automatically renumbered

### Deleting a Record
1. Select the row to delete
2. Click **Delete** button
3. Confirm the deletion
4. All subsequent rows will be automatically renumbered

### Reordering Records
1. Select a row
2. Use **Up** ↑ or **Down** ↓ arrow buttons
3. Rows swap positions (Position column remains sequential)
4. Click **Update** to save changes

### Filtering Records
1. Enter search terms in:
   - **Band** textbox (partial match)
   - **Song** textbox (partial match)
   - **Viewed** checkbox (Checked/Unchecked/Indeterminate)
2. Click **Filter** to apply
3. Click **Clear** to reset filters

> **Note:** Editing, reordering, adding, and deleting are disabled while filters are active. Clear filters first.

### Key Features Implementation

#### Change Tracking
- In-memory `DataTable` for grid operations
- `List<GreatestHitModel>` for business logic
- Visual feedback with yellow row highlighting
- Enable/disable Update button based on state

#### Excel Operations
- Read: Load entire first sheet into `DataTable`
- Write: Overwrite Excel file with current `DataTable` state
- Validation: Row count, header presence, column count
- Format Support: Auto-detection of `.xls` vs `.xlsx`

#### Data Integrity
- Position column auto-renumbering on insert/delete
- Row swapping maintains sequential positions
- Filter state prevents conflicting operations
- Unsaved changes prompt on file load and application exit

## 📚 Version History

### v1.2.0 (Current)
**User can update row position of a track**: Editing the row of a row causes affected rows to be updated as well

### v1.1.0 (Current)
**Major Feature Addition: Full CRUD Support**
- ➕ **Add Record**: Insert new records at any position with automatic renumbering
- 🗑️ **Delete Record**: Remove records with confirmation and automatic renumbering
- 🔍 **Advanced Filtering**: Multi-criteria filtering by band, song, and viewed status
- 🎨 **UX Improvements**: Visual feedback for unsaved changes (yellow highlight)
- ⚙️ **Configurable Auto-Save**: Toggle instant save vs. manual save mode
- 🔒 **Filter Safety**: Prevent editing operations while filters are active
- 📝 **Logging Integration**: Comprehensive Serilog implementation

**Breaking Changes:**
- Configuration now required in `appsettings.json`
- `SaveToExcelInstantly` parameter added

### v1.0.0 (Initial Release)
**Core Functionality**
- 📂 **Load Excel File**: Open `.xlsx` and `.xls` files
- ✏️ **Edit Records**: Double-click to edit, with OK/Cancel dialog
- 🔄 **Reorder Rows**: Move records up/down with arrow buttons
- 💾 **Manual Save**: Update button to commit all changes
- ❌ **Discard Changes**: Revert all unsaved modifications
- ⚠️ **Validation**: 500-row limit enforcement
- 🔔 **Unsaved Changes Warning**: Alerts on file load and app exit

**Technical Stack (v1.0.0):**
- WinForms on .NET 8
- NPOI for Excel operations
- Manual save workflow
- Basic grid display with row selection

**Limitations (v1.0.0):**
- No add/delete functionality
- No filtering or search capabilities
- Manual save only (no auto-save option)
- Limited validation and error handling

## 🔮 Roadmap

### Planned Features
- [ ] **Precise Row Upsertion**: Insert or update at exact position with UI input
- [ ] **Database Backend**: Option to save data to SQL database instead of Excel
- [ ] **Export Options**: Export to CSV, JSON formats
- [ ] **Sorting**: Column-based sorting in grid
- [ ] **Undo/Redo**: Operation history management
- [ ] **Bulk Operations**: Multi-row selection and batch editing
- [ ] **Import from URL**: Auto-fetch video metadata from URLs
- [ ] **Dark Mode**: Theme support

### Known Issues
- Filters must be cleared before CRUD operations
- Large files (>500 rows) are rejected entirely (no partial load)

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [NPOI](https://github.com/nissl-lab/npoi) - .NET Excel library
- [Serilog](https://serilog.net/) - Logging framework
- README file developed with assistance from ChatGPT

## 📧 Contact

**Project Maintainer**: BillSig  
**Repository**: [https://github.com/BillSig/Top100](https://github.com/BillSig/Top100)

---

**Note**: This application is designed for personal use to manage a curated music collection. Excel file location references and video storage paths are configured for local development environments.
