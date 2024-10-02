# 📇 Contact Manager

This .NET web application allows users to manage their contacts. Users can upload CSV files containing contact details, store the data in an MS SQL database, and view the stored data on the web page.

## 🚀 Features

- **CSV Upload**: Users can upload a CSV file containing the following fields:
  - **Name**: The name of the contact (string)
  - **Date of Birth**: The birth date of the contact (date)
  - **Married**: Marital status (boolean)
  - **Phone**: Contact number (string)
  - **Salary**: Monthly salary (decimal)

- **Data Display**: View all stored contacts in a user-friendly table.

- **Client-Side Filtering**: Easily filter data by any column to find specific contacts. 🔍

- **Client-Side Sorting**: Sort contacts by any field to organize your data effectively. 📊

- **Inline Editing**: Edit any row directly in the table for quick updates. ✏️

- **Delete Functionality**: Remove records from the database effortlessly. 🗑️

- **Data Validation**: Implement validation to ensure data integrity. ✔️

## 🛠️ Technologies Used

- .NET Core
- MS SQL Database
- JavaScript (for client-side functionalities)

## 💾 Database Backup

A backup of the database is included in the repository. Make sure to restore it to your local SQL Server instance for testing purposes.
