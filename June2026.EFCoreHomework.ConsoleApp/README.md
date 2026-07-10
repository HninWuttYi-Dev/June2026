# EF Core Scaffolding Behavior - Assignment

This repository contains the homework assignment assigned by Instructor Ko Lin to demonstrate how Entity Framework Core Scaffolding behaves when database schemas change (specifically when tables are added or deleted).

##  Assignment Task
1. **First step:** Create the first table, run the Scaffold command in the Console, and implement a Read-Only CRUD operation.
2. **Second step:** Create a second table, run Scaffold again, and implement its Read-Only operation.
3. **Third step:** Delete the second table from the database and create a third table. Run Scaffold again, write the Read-Only operation.
4. **Question:** Does the Scaffold still work after the final test?

---

## Execution & Experience

### Step 1: Initial Setup
- Created `Tbl_Customer` in SQL Server.
- Scaffolded the database into a separate Class Library (`June2026.EFCoreHomework.Database`) to maintain a clean architecture.
- Successfully queried the data using `HomeworkAppDbContext` in the Console App.

### Step 2: The Scaffold Overwrite Trap
- Created `Tbl_Department`.
- **Experience/Issue Faced:** When I initially ran the scaffold command using the `-t Tbl_Department -f` flag, I noticed that my previous `Tbl_Customer` completely disappeared from the `DbContext`! 
- **Solution:** I learned that when using the `-t` (table) flag with `-f` (force), EF Core will *only* scaffold the specific tables listed and will erase the unlisted ones from the context. To fix this, I had to explicitly chain both tables: `-t Tbl_Customer -t Tbl_Department`.

### Step 3: Deleting the Table
- Dropped `Tbl_Department` in SQL and created `Tbl_Vehicle`.
- Ran the scaffold command again for the remaining tables (`Tbl_Customer` and `Tbl_Vehicle`).

---

##  Answer to the Instructor's Question
**Question:** *Does Scaffold still work after the final test?*

**Answer:** Yes, the Scaffold command works perfectly, **but it intentionally breaks the existing C# code.**

When the second table (`Tbl_Department`) was deleted from the database and scaffold was re-run, EF Core overwrote the `HomeworkAppDbContext.cs` file. Because the table no longer exists in SQL, EF Core removed `DbSet<TblDepartment>` from the context.

This resulted in a **compile-time error** in my `Program.cs` file because the application was still trying to read from `db.TblDepartments`, which no longer exists. 

**An interesting observation:** EF Core does *not* delete the orphaned `.cs` model files. The `TblDepartment.cs` file was left sitting in the Models folder, but without the `DbSet` connection in the context, it is completely useless to the application.