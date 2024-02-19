# TaskManagementApp

Task Management App is a simple task management application built using .NET Core. It allows users to manage their tasks efficiently with features similar to popular task management tools like Jira, Trello, or Basecamp.

## Features

- Users can add new tasks to the task board with details including name, description, and deadline.
- Users can edit tasks and update all details.
- Users can delete tasks.
- Users can attach images to tasks.
- Users can view detailed information about each task.
- Users can add columns to the task board representing different work states (e.g., To Do, In Progress, Done).
- Users can move tasks between columns.
- Users can sort tasks in each column alphabetically by name, with favorited tasks sorted to the top every time.

## Setup

To run the Task Management App locally, follow these steps:

1. Clone this repository to your local machine.
2. Open the project in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
3. Build the project to restore dependencies.
4. Set up the database connection string in the `appsettings.json` file.
5. Run the database migrations to create the necessary tables.
6. Start the application.

## Technologies Used

- .NET Core
- Azure APIs
- NUnit

## Contributing

Contributions are welcome! If you find any bugs or have suggestions for improvement, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).